using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Operation.FileExt.Core
{
    public interface IFileTask : IDisposable
    {
        int ScanPeriod { get; set; }

        void Move(string srcPath, string dstPath);
        void Delete(string path);
    }
}
