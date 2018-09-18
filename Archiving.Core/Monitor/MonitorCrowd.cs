using Archiving.Entity;
using Archiving.Observer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Archiving.Core.Options;
using Archiving.Core;

namespace Archiving.Monitor
{
    public interface IPathMonitorCrowd : IObservable<FileEntity>, IDisposable
    {
        void Start();
    }

    public sealed class MonitorCrowd : IPathMonitorCrowd
    {
        private readonly int _readSpinTimeout;
        private readonly IEnumerable<MonitorOptions> _scs;
        private readonly ILogger<MonitorCrowd> _log;
        private readonly IEnumerable<IPathMonitor> _monitors;
        private readonly List<IPathObserver> _observers;
        public MonitorCrowd(
            IOptions<AppOptions> options,
            ILogger<MonitorCrowd> log)
        {
            NamedNullException.Assert(options, nameof(options));

            _readSpinTimeout = options.Value.FileReadSpinWaitTimeout;
            _scs = options.Value.Groups.Select(x => x.Monitor).ToList();
            _log = NamedNullException.Assert(log, nameof(log));
            _monitors = _scs.Select(x => x.Path.ToMonitoredPath())
                .Distinct(MonitoredPath.Comparer)
                .Select(x => new PathMonitor(x))
                .ToList();
            _observers = new List<IPathObserver>();
        }

        public void Start()
        {
            foreach (var monitor in _monitors)
            {
                try
                {
                    monitor.Inform += informed;
                    monitor.Start();
                }
                catch (Exception ex)
                {
                    monitor.Inform -= informed;
                    _log.LogError(ex, "Some errors occur when start monitors.");
                }
            }
        }

        void informed(object sender, FileEntity entity)
        {
            if (!SpinWait.SpinUntil(
                () => tryVisitFile(entity),
                _readSpinTimeout))
                _log.LogError(
                    Core.Properties.Resources.ConflictIOExceptionString,
                    entity.FileName,
                    _readSpinTimeout);

            route2Do(resolve(entity), entity);

            #region LocalFunctions
            IEnumerable<IPathObserver> resolve(FileEntity entity1)
            {
                return _observers.Where(x => x.Filter.IsMatch(entity1)).ToList();
            }

            void route2Do(IEnumerable<IPathObserver> obs, FileEntity entity1)
            {
                foreach (var ob in obs)
                {
                    try
                    {
                        ob.OnNext(entity1);
                        if (entity1.Locked) break;
                    }
                    catch (AggregateException ae)
                    {
                        _log.LogAggregateException(ae);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError(ex, "Single exception occurs.");
                    }
                }
            }

            bool tryVisitFile(FileEntity entity1)
            {
                try
                {
                    using (var fs = new FileStream(entity1.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 1)) { }
                    return true;
                }
                catch (IOException)
                {
                    return false;
                }
            }
            #endregion LocalFunctions
        }

        public IDisposable Subscribe(IObserver<FileEntity> observer)
        {
            var ob = observer as IPathObserver;

            _observers.Add(ob);
            return new PathMonitorDisposableObject(this, ob);
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
                    foreach (var monitor in _monitors)
                    {
                        monitor.Inform -= informed;
                        monitor.Dispose();
                    }
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~MonitorCluster() {
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

        private class PathMonitorDisposableObject : IDisposable
        {
            MonitorCrowd _router;
            IPathObserver _observer;
            public PathMonitorDisposableObject(MonitorCrowd router, IPathObserver observer)
            {
                _router = router;
                _observer = observer;
            }

            #region IDisposable Support
            private bool disposedValue = false; // 要检测冗余调用

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: 释放托管状态(托管对象)。
                        _router?._observers?.Remove(_observer);
                    }

                    // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                    // TODO: 将大型字段设置为 null。

                    disposedValue = true;
                }
            }

            // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
            // ~PathMonitorDisposableObject() {
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
}
