using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archiving.Entity;
using System.IO;
using Archiving.Operation.FileExt.Core;
using Archiving.Core.Operation;
using Archiving.Core.Options;
using Archiving.Core;

namespace Archiving.Operation.FileExt
{
    public class MoveFileOperation : IObserverOperation
    {
        private readonly IFileTask _man;
        private readonly OperationOptions _options;

        public MoveFileOperation(
            IFileTask man,
            OperationOptions options)
        {
            _man = NamedNullException.Assert(man, nameof(man));
            NamedNullException.Assert(options, nameof(options));
            NotTrueException.Assert(options.DoMove, nameof(options.DoMove));
            StringNullOrEmptyException.Assert(options.MovePath, nameof(options.MovePath));

            _options = options;
        }

        public bool LockResource => true;
        public int Priority => PriorityDefinationRefTable.FileMove;

        public void Handle(FileEntity entity)
        {
            var filePath = Path.Combine(
                 _options.MovePath,
                 entity.FileName.GetFileName(!_options.DisableFileNameAddedTimeStamp));
            _man.Move(entity.FullPath, filePath);
            entity.LockBy(this);
        }

        public void Dispose() { }
    }
}
