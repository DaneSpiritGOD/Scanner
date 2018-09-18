using Archiving.Core.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archiving.Core.Operation
{
    public interface IObserverOperationFactoryExtension
    {
        IObserverOperation Create(string groupName, OperationOptions oo);
    }
}
