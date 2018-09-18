using System;
using System.Collections.Generic;
using System.Text;

namespace Archiving.Core.Options
{
    public class OptionsPostException : RuntimeException
    {
        public OptionsPostException(string msg) : base(msg) { }
    }
}
