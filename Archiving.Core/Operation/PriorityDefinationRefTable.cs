using System;
using System.Collections.Generic;
using System.Text;

namespace Archiving.Core.Operation
{
    /// <summary>
    /// 越小优先级越高
    /// </summary>
    public static class PriorityDefinationRefTable
    {
        public const int NetTransfer = 1000;
        public const int Ftp = NetTransfer;

        public const int FileMove = 2000;
        public const int FileBackup = FileMove;
        public const int FileDelete = FileMove + 1;

        public const int Composite = None - 1;

        public const int None = int.MaxValue;
    }
}
