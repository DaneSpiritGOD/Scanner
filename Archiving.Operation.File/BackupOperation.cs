using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Archiving.Core;
using Archiving.Core.Operation;
using Archiving.Core.Options;
using Archiving.Entity;
using Archiving.Operation.FileExt.Core;

namespace Archiving.Operation.FileExt
{
    public class BackupOperation : IObserverOperation
    {
        private readonly OperationOptions _options;
        private readonly string _backupDirLinks;
        private readonly IFileTask _man;

        public int Priority => PriorityDefinationRefTable.FileBackup;

        public bool LockResource => true;

        public BackupOperation(
            string groupName,
            IFileTask man,
            OperationOptions options)
        {
            _backupDirLinks = StringNullOrEmptyException.Assert(groupName, nameof(groupName));
            _man = NamedNullException.Assert(man, nameof(man));
            NamedNullException.Assert(options, nameof(options));
            NotTrueException.Assert(options.DoBackup, nameof(options.DoBackup));
            StringNullOrEmptyException.Assert(options.BackupPath, nameof(options.BackupPath));

            _options = options;
        }

        public void Dispose() { }

        public void Handle(FileEntity entity)
        {
            var dstPath = getRealBackupPath(entity);
            _man.Move(entity.FullPath, dstPath);
            entity.LockBy(this);

            string getRealBackupPath(FileEntity entity1) =>
            Path.Combine(
                _options.BackupPath,
                _backupDirLinks,
                entity1.NewTime.ToString("yy-MM-dd"),
                entity1.FileName.GetFileName(!_options.DisableFileNameAddedTimeStamp));
        }
    }
}
