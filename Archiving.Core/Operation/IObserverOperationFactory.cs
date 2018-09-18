using Archiving.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiving.Core.Operation
{
    public interface IObserverOperationFactory
    {
        IObserverOperation Create(GroupOptions go);
    }
}
