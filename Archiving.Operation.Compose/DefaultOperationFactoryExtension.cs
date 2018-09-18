using Archiving.Operation;
using Archiving.Operation.FileExt;
using Archiving.Operation.FileExt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archiving.Core.Options;
using Archiving.Operation.Net;
using Archiving.Operation.Ftp;
using Microsoft.Extensions.Logging;
using Archiving.Core.Operation;

namespace Archiving.Operation.Compose
{
    public class DefaultOperationFactoryExtension : IObserverOperationFactoryExtension
    {
        private readonly IFileTask _man;
        private readonly ILoggerFactory _loggerFactory;

        public DefaultOperationFactoryExtension(
            IFileTask fileMan,
            ILoggerFactory loggerFactory)
        {
            _man = NamedNullException.Assert(fileMan, nameof(fileMan));
            _loggerFactory = NamedNullException.Assert(loggerFactory, nameof(loggerFactory));
        }

        public IObserverOperation Create(string groupName, OperationOptions oo)
        {
            if (oo.DoBackup) return new BackupOperation(groupName, _man, oo);
            if (oo.DoDelete) return new DeleteFileOperation(_man, oo);
            if (oo.DoFtpTransfer) return new FtpTransferOperation(oo, _loggerFactory);
            if (oo.DoMove) return new MoveFileOperation(_man, oo);
            if (oo.DoNetTransfer) return new NetTransferOperation(oo);

            return default;
        }
    }
}
