using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archiving.Core.Operation;
using Archiving.Entity;

namespace Archiving.Core.Operation
{
    internal class NoOperation : IObserverOperation
    {
        public int Priority => PriorityDefinationRefTable.None;
        public bool LockResource => false;
        public void Dispose() { }
        public void Handle(FileEntity entity) { }
    }
}
