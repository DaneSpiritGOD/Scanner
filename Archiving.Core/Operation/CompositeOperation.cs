using Archiving.Core.Operation;
using Archiving.Entity;
using Archiving.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Operation
{
    public sealed class CompositeOperation : IObserverOperation
    {
        private readonly List<IObserverOperation> _operations;
        public CompositeOperation(params IObserverOperation[] operations)
        {
            _operations = new List<IObserverOperation>();
            //var _lockResource = false;
            for (var index = 0; index < operations.Length; ++index)
            {
                _operations.Add(NamedNullException.Assert(operations[index], "operation"));
                //_lockResource |= _operations[index].LockResource;
            }
            //LockResource = _lockResource;
        }

        public bool LockResource { get; }
        public int Priority => PriorityDefinationRefTable.Composite;

        public void Handle(FileEntity entity)
        {
            var exs = new List<Exception>();
            for (var index = 0; index < _operations.Count; ++index)
            {
                try
                {
                    _operations[index]?.Handle(entity);
                    //if (_operations[index].LockResource) break;
                    if (entity.Locked) break;
                }
                catch (Exception ex)
                {
                    exs.Add(ex);
                }
            }
            if (exs.Count != 0) throw new AggregateException("Some of Composite Operations Error", exs);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    for (var index = 0; index < _operations.Count; ++index)
                    {
                        _operations[index]?.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CompositeOperation() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
