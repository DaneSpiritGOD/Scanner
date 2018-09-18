using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archiving.Entity
{
    public class MonitoredPath
    {
        public string Path { get; }
        public static IEqualityComparer<MonitoredPath> Comparer => MonitoredPathEqualityComparer.Default;
        public bool Equals(MonitoredPath path) => Comparer.Equals(path, this);

        public MonitoredPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException(nameof(path) + " is null or empty.");

            path.EnsureDirExists();
            Path = System.IO.Path.GetFullPath(path).ToLower();
        }

        private class MonitoredPathEqualityComparer : IEqualityComparer<MonitoredPath>
        {
            public static readonly MonitoredPathEqualityComparer Default = new MonitoredPathEqualityComparer();

            StringComparer _innerComparer = StringComparer.OrdinalIgnoreCase;
            private MonitoredPathEqualityComparer() { }

            public bool Equals(MonitoredPath x, MonitoredPath y)
            {
                return _innerComparer.Equals(x.Path, y.Path);
            }

            public int GetHashCode(MonitoredPath obj)
            {
                return _innerComparer.GetHashCode(obj.Path);
            }
        }
    }
}
