using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Entity
{
    public static class MonitoredPathExtensions
    {
        public static MonitoredPath ToMonitoredPath(this string path) => new MonitoredPath(path);

        public static bool MonitoredPathEquals(this string path1, string path2) => (new MonitoredPath(path1)).Equals(new MonitoredPath(path2));

        public static bool Equals(this string path, MonitoredPath mpath) => new MonitoredPath(path).Equals(mpath);

        public static bool Equals(this MonitoredPath mpath, string path) => new MonitoredPath(path).Equals(mpath);

    }
}
