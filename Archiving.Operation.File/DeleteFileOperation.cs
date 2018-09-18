using Archiving.Core.Operation;
using Archiving.Entity;
using Archiving.Operation;
using Archiving.Operation.FileExt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archiving.Core.Options;

namespace Archiving.Operation.FileExt
{
    public class DeleteFileOperation : IObserverOperation
    {
        private readonly IFileTask _man;

        public DeleteFileOperation(
            IFileTask man,
            OperationOptions options)
        {
            _man = NamedNullException.Assert(man, nameof(man));
            NamedNullException.Assert(options, nameof(options));
            NotTrueException.Assert(options.DoDelete, nameof(options.DoDelete));
        }

        public void Dispose() { }

        public bool LockResource => true;
        public int Priority => PriorityDefinationRefTable.FileDelete;

        public void Handle(FileEntity entity)
        {
            _man.Delete(entity.FullPath);
            entity.LockBy(this);
        }
    }
}
