using Archiving.Monitor;
using Archiving.Observer;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Archiving.ConfigureService
{
    public class NewFileScanHostedService : BackgroundService
    {
        private readonly IPathMonitorCrowd _monitor;
        private readonly IObserverProvider _observerProvider;

        public NewFileScanHostedService(
            IPathMonitorCrowd monitorCrowd,
            IObserverProvider observerProvider)
        {
            _monitor = NamedNullException.Assert(monitorCrowd, nameof(monitorCrowd));
            _observerProvider = NamedNullException.Assert(observerProvider, nameof(observerProvider));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _observerProvider.Init(_monitor);
            _monitor.Start();
            return Task.CompletedTask;
        }
    }
}
