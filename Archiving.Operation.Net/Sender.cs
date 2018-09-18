using NanomsgPlus.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Operation.Net
{
    internal class Sender : IDisposable
    {
        PushSocket _socket;
        public Sender(string addr)
        {
            StringNullOrEmptyException.Assert(addr, nameof(addr));

            _socket = new PushSocket();
            _socket.Options.SendTimeout = TimeSpan.FromMilliseconds(100);
            _socket.Options.ReconnectInterval = TimeSpan.FromMilliseconds(150);
            _socket.Connect(addr.ToLower());
        }

        public void Send(byte[] data)
        {
            _socket.Send(data);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _socket.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Sender() {
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
