using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Monitor
{
    public interface IMonitor<TResourcePath,TResource> : IDisposable, IInform<TResource>
    {
        TResourcePath ResourcePath { get; }
        void Start();
    }

    public interface IMonitorFactory<TResourcePath, TMonitor>
    {
        TMonitor Create(TResourcePath path);
    }

    public interface IInform<T>
    {
        event EventHandler<T> Inform;
    }
}
