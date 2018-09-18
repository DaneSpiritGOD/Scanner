using Archiving.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Monitor
{
    public interface IPathMonitor : IMonitor<MonitoredPath, FileEntity> { }
}
