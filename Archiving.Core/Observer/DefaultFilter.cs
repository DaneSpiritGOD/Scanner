using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Archiving.Monitor;
using Archiving.Entity;
using Archiving.Core.Options;

namespace Archiving.Observer
{
    internal class DefaultFilter : IFilter
    {
        MonitorOptions _config;

        public DefaultFilter(MonitorOptions config)
        {
            _config = config ?? throw new ArgumentException(nameof(config) + " is null or empty.");
        }

        public bool IsMatch(FileEntity exp)
        {
            return sameRootDir(exp) && fileNameKeyFilter(exp) && withExtension(exp);
        }

        private bool sameRootDir(FileEntity exp)
        {
            return _config.Path.MonitoredPathEquals(exp.DirectoryPath);
        }

        private bool fileNameKeyFilter(FileEntity exp)
        {
            return string.IsNullOrWhiteSpace(_config.FileNameKey) ||
                _config.FileNameKey.Contains("*") ||
                exp.FileName.StartsWith(_config.FileNameKey, StringComparison.OrdinalIgnoreCase);
        }

        private bool withExtension(FileEntity exp)
        {
            return _config.FileExtensions == null ||
                _config.FileExtensions.Count() == 0 ||
                _config.FileExtensions.Any(x => x.Contains("*") ||
                Path.GetExtension(exp.FullPath).EndsWith(x, StringComparison.OrdinalIgnoreCase));
        }
    }
}
