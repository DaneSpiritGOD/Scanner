using Suites.Core;
using Archiving.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Archiving.Monitor
{
    internal class PathMonitor : IPathMonitor
    {
        public MonitoredPath ResourcePath { get; }

        public PathMonitor(MonitoredPath path)
        {
            ResourcePath = path ?? throw new ArgumentNullException(nameof(path) + " is null.");
        }

        bool _isBegan = false;
        public void Start()
        {
            if (_isBegan) throw new HaveBeganMonitoredException();
            dealWithExistingFiles();
            initInnerWatcher();
            _isBegan = true;
        }

        private void dealWithExistingFiles()
        {
            if (!ResourcePath.Path.EnsureDirExists()) return;

            var srcDirInfo = new DirectoryInfo(ResourcePath.Path);
            var fileInfos = srcDirInfo.GetFiles();
            foreach (var fileInfo in fileInfos)
            {
                foundNewFile(FileEntity.Create(fileInfo));
            }
        }

        public event EventHandler<FileEntity> Inform;
        private void foundNewFile(FileEntity value)
        {
            Inform?.Invoke(this, value);
        }

        #region FileSystemWatcher
        private FileSystemWatcher _fileWatcher;
        private void initInnerWatcher()
        {
            //监控文件夹变化
            _fileWatcher = new FileSystemWatcher(ResourcePath.Path);
            _fileWatcher.Created += fileCreated;
            //watcher.Changed += p_Changed;
            //watcher.Deleted += p_Deleted;
            _fileWatcher.IncludeSubdirectories = false;
            _fileWatcher.EnableRaisingEvents = true;
        }

        private void fileCreated(object sender, FileSystemEventArgs e)
        {
            //foundNewFile(FileEntity.Create(e));
            Task.Run(() => foundNewFile(FileEntity.Create(e)));
        }
        #endregion FileSystemWatcher

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    if (_fileWatcher != null)
                    {
                        _fileWatcher.Created -= fileCreated;
                        _fileWatcher.Dispose();
                        _fileWatcher = null;
                    }
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~PathMonitor() {
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

    public class HaveBeganMonitoredException : Exception
    {
        public HaveBeganMonitoredException() : base(Core.Properties.Resources.HaveBeganMonitor) { }
    }
}
