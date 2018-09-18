using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archiving.Monitor;
using Archiving.Entity;
using Archiving.Operation;
using Archiving.Core.Options;
using Archiving.Core.Operation;

namespace Archiving.Observer
{
    public interface IPathObserver : IObserver<FileEntity>, IDisposable
    {
        IFilter Filter { get; }
    }

    public sealed class DefaultObserver : IPathObserver
    {
        IDisposable _busDisposableObject;
        public DefaultObserver(
            IPathMonitorCrowd monitorCrowd,
            IFilter filter,
            IObserverOperation operation)
        {
            if (monitorCrowd == null)
                throw new ArgumentNullException(nameof(monitorCrowd) + " is null.");

            Filter = filter ?? throw new ArgumentNullException(nameof(filter) + " is null.");
            Operation = operation ?? throw new ArgumentNullException(nameof(operation) + " is null.");
            _busDisposableObject = monitorCrowd.Subscribe(this);
        }

        IObserverOperation Operation { get; }
        public IFilter Filter { get; }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 内部没有做过滤，需要外部调用者提前过滤
        /// </summary>
        /// <param name="value"></param>
        public void OnNext(FileEntity value)
        {
            Operation.Handle(value);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    _busDisposableObject?.Dispose();
                    Operation?.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~DefaultObserver() {
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
