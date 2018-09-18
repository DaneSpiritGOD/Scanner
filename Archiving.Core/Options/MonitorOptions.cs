using Archiving.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Core.Options
{
    public class MonitorOptions
    {
        public string Path { get; set; }
        public string FileNameKey { get; set; }
        public IEnumerable<string> FileExtensions { get; set; }
    }
}
