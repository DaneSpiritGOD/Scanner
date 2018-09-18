using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Options;

namespace Archiving.Operation.FileExt.Core
{
    public sealed class FileTask : IFileTask
    {
        /// <summary>
        /// 内部Timer的循环周期,单位ms
        /// </summary>
        public int ScanPeriod { get; set; }

        private Timer _timer;
        private readonly object _syncRoot;
        private readonly ConcurrentQueue<(string SrcPath, string DestPath)> _movingQueue;
        //private readonly ConcurrentQueue<string> _deletingQueue;
        public FileTask(IOptions<FileTaskOptions> options)
        {
            ScanPeriod = NamedNullException.Assert(options, nameof(options)).Value.ScanPeriod;
            _syncRoot = new object();
            _movingQueue = new ConcurrentQueue<(string SrcPath, string DestPath)>();
            //_deletingQueue = new ConcurrentQueue<string>();
        }

        private void initTimer()
        {
            if (_timer != null) return;
            lock (_syncRoot)
            {
                if (_timer != null) return;
                _timer = new Timer(loop, null, 50, ScanPeriod);
            }
        }

        private void loop(object state)
        {
            if (!_movingQueue.TryDequeue(out var pair)) return;

            var src = pair.SrcPath;
            var dst = pair.DestPath;
            if (string.IsNullOrEmpty(dst))
            {
                if (!tryDelete(dst)) _movingQueue.Enqueue(pair);
            }
            else
            {
                if (!tryMove(src, dst)) _movingQueue.Enqueue(pair);
            }

            //if (_movingQueue.TryDequeue(out var pair1) && !tryMove(pair1.SrcPath, pair1.DestPath)) _movingQueue.Enqueue(pair1);
            //if (_deletingQueue.TryDequeue(out var pathToDel) && !tryDelete(pathToDel)) _deletingQueue.Enqueue(pathToDel);
        }

        public void Move(string srcPath, string dstPath)
        {
            StringNullOrEmptyException.Assert(srcPath, nameof(srcPath));
            StringNullOrEmptyException.Assert(dstPath, nameof(dstPath));

            if (tryMove(srcPath, dstPath)) return;

            initTimer();
            _movingQueue.Enqueue((srcPath, dstPath));
        }

        public void Delete(string path)
        {
            StringNullOrEmptyException.Assert(path, nameof(path));

            if (tryDelete(path)) return;

            initTimer();
            //_deletingQueue.Enqueue(path);
            _movingQueue.Enqueue((path, string.Empty));
        }

        private static bool tryMove(string localSrcPath, string localDstPath)
        {
            try
            {
                Path.GetDirectoryName(localDstPath).EnsureDirExists();
                File.Move(localSrcPath, localDstPath);
                return true;
            }
            catch (FileNotFoundException)
            {
                return true;
            }
            catch (IOException ex) when (ex.HResult == -2147024864)
            {
                return false;
            }
        }

        private static bool tryDelete(string localSrcPath)
        {
            try
            {
                //if (File.Exists(localSrcPath)) return true;
                File.Delete(localSrcPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    _timer?.Dispose();
                    _timer = null;
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~DefaultFileOperationManager() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
