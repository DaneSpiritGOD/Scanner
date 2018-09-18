using System;
using System.Collections.Generic;
using System.Text;

namespace Archiving.Core.Options
{
    public class AppOptions
    {
        public IEnumerable<GroupOptions> Groups { get; set; }
        public DefaultOptions Default { get; set; }

        /// <summary>
        /// 目录新建时被人家读取后的自旋时间，不宜太长
        /// </summary>
        public int FileReadSpinWaitTimeout { get; set; } = 500;
        public const int MaxFileReadSpinWaitTimeout = 2000;
    }
}
