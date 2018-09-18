using Archiving.Core.Options;
using Archiving.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Observer
{
    public interface IFilter<T>
    {
        bool IsMatch(T exp);
    }

    public interface IFilter : IFilter<FileEntity> { }
}
