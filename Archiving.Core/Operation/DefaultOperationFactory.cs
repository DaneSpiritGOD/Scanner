using Archiving.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archiving.Core.Options;
using Microsoft.Extensions.Logging;
using Archiving.Core.Operation;

namespace Archiving.Operation.Internal
{
    public class DefaultOperationFactory : IObserverOperationFactory
    {
        private readonly IEnumerable<IObserverOperationFactoryExtension> _extensions;

        public DefaultOperationFactory(
            IEnumerable<IObserverOperationFactoryExtension> extensions)
        {
            _extensions = extensions;
        }

        public IObserverOperation Create(GroupOptions go)
        {
            if (go.Operations.Count() == 0) return new NoOperation();

            return new CompositeOperation(
                go.Operations
                    .Select(
                        oo =>
                        _extensions.Select(x => x.Create(go.Name, oo))
                    .Where(x => x != default).SingleOrDefault() ?? new NoOperation())
                    .ToArray());
        }
    }
}
