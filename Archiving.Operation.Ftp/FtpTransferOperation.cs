using Archiving.Core.Operation;
using Archiving.Core.Options;
using Archiving.Entity;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Archiving.Operation.Ftp
{
    public class FtpTransferOperation : IObserverOperation
    {
        private readonly int _timeout;
        private readonly SimpleFtpClient _ftpClient;
        private readonly ILogger<FtpTransferOperation> _logger;
        private readonly TimeoutPolicy _timeoutPolicy;
        private readonly OperationOptions _options;

        public int Priority => PriorityDefinationRefTable.Ftp;

        public bool LockResource => false;

        public FtpTransferOperation(
            OperationOptions oo,
            ILoggerFactory loggerFactory)
        {
            _options = NamedNullException.Assert(oo, nameof(oo));
            var fo = _options.Ftp;
            NamedNullException.Assert(fo, nameof(fo));
            _timeout = fo.Timeout.HasValue ? fo.Timeout.Value : FtpOptions.MaxFtpTimeout;
            _ftpClient = new SimpleFtpClient(fo.FtpRoot, fo.UserName, fo.Password, _timeout);

            _logger = NamedNullException.Assert(loggerFactory, nameof(loggerFactory)).CreateLogger<FtpTransferOperation>();
            _timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromMilliseconds(_timeout), TimeoutStrategy.Pessimistic);
        }

        public void Dispose() { }

        public void Handle(FileEntity entity)
        {
            _timeoutPolicy.ExecuteAsync(
                async ct =>
                {
                    try
                    {
                        await _ftpClient.UploadAsync(
                            entity.FullPath,
                            entity.FileName.GetFileName(!_options.DisableFileNameAddedTimeStamp));
                    }
                    catch (WebException we) when (we.Status == WebExceptionStatus.ConnectFailure)
                    {
                        _logger.LogError("注意：当前FTP 服务器连接不可用，为了保证传图顺利，请立即联系技术人员。");
                    }
                    catch (WebException we2)
                    {
                        _logger.LogError(we2, "其他Ftp错误。");
                    }
                },
                CancellationToken.None)
            .GetAwaiter()
            .GetResult();
        }
    }
}
