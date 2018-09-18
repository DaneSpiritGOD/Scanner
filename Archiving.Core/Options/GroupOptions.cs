using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Core.Options
{
    public class GroupOptions
    {
        public string Name { get; set; }
        public MonitorOptions Monitor { get; set; }
        public IEnumerable<OperationOptions> Operations { get; set; }
    }
}
