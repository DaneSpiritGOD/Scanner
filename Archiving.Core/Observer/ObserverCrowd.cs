using Archiving.Entity;
using Archiving.Monitor;
using Archiving.Operation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archiving.Core.Options;
using Archiving.Core.Operation;

namespace Archiving.Observer
{
    public interface IObserverProvider
    {
        void Init(IPathMonitorCrowd monitorCrowd);
    }

    public sealed class ObserverCrowd : IObserverProvider
    {
        readonly IObserverOperationFactory _operationFactory;
        private readonly IEnumerable<GroupOptions> _gps;
        private IEnumerable<IPathObserver> _observers;

        public ObserverCrowd(
            IOptions<AppOptions> options,
            IObserverOperationFactory operationFactory)
        {
            _operationFactory = NamedNullException.Assert(operationFactory, nameof(operationFactory));
            _gps = NamedNullException.Assert(options, nameof(options)).Value.Groups;
        }

        public void Init(IPathMonitorCrowd monitorCrowd)
        {
            if (_observers != null) return;

            _observers = _gps.Select(
                x =>
                 new DefaultObserver(
                     monitorCrowd,
                     new DefaultFilter(x.Monitor),
                     _operationFactory.Create(x)))
                 .ToList();
        }
    }
}
