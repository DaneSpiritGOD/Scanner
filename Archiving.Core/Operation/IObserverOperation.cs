using Archiving.Core.Options;
using Archiving.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Core.Operation
{
    public interface IObserverOperation<T> : IDisposable
    {
        //int Priority { get; }

        void Handle(T entity);
        //bool LockResource { get; }
    }

    public interface IObserverOperation : IObserverOperation<FileEntity> { }
}
