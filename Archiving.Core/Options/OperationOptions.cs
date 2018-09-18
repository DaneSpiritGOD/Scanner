using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Core.Options
{
    public class OperationOptions : RawOperation
    {
        public bool DoNetTransfer => EnableNetTransfer && !string.IsNullOrWhiteSpace(NetAddress);
        public bool EnableNetTransfer { get; set; }

        public bool DoFtpTransfer => EnableFtpTransfer && (Ftp != null);
        public bool EnableFtpTransfer { get; set; }

        public bool DoMove => EnableMove && !string.IsNullOrWhiteSpace(MovePath);
        public bool EnableMove { get; set; }

        public bool DoBackup => EnableBackup && !string.IsNullOrWhiteSpace(BackupPath);
        public bool EnableBackup { get; set; }

        public bool DoDelete => EnableDelete;
        public bool EnableDelete { get; set; }

        public bool DisableFileNameAddedTimeStamp { get; set; }

        public bool DoInProcessTransfer => !string.IsNullOrWhiteSpace(InProcessTransferKey);
        public string InProcessTransferKey { get; set; }
    }

    public class FtpOptions
    {
        public string FtpRoot { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Timeout { get; set; }

        public const int MaxFtpTimeout = 100000;
        public static void EnsureValid(FtpOptions ftp)
        {
            NamedNullException.Assert(ftp, nameof(ftp));
            StringNullOrEmptyException.Assert(ftp.FtpRoot, nameof(ftp.FtpRoot));
            StringNullOrEmptyException.Assert(ftp.UserName, nameof(ftp.UserName));
            NotTrueException.Assert(ftp.Timeout.HasValue, nameof(ftp.Timeout.HasValue));
            NumberOutOfRangeException<int>.Assert(ftp.Timeout.Value, 0, MaxFtpTimeout, nameof(ftp.Timeout.Value));
        }

        public static bool CheckValid(FtpOptions ftp)
        {
            try
            {
                EnsureValid(ftp);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class RawOperation
    {
        public string NetAddress { get; set; }
        public FtpOptions Ftp { get; set; }
        public string MovePath { get; set; }
        public string BackupPath { get; set; }
    }
}
