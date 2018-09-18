using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Archiving.Core.Options;
using Archiving.Operation.FileExt.Core;
using Archiving.Monitor;
using Archiving.Observer;
using Archiving.Operation;
using Archiving.Operation.Compose;
using Archiving.Core.Operation;
using Archiving.ConfigureService;
using Archiving.Operation.Internal;

namespace Microsoft.Extensions.Hosting
{
    public static class ConfigureExtensions
    {
        private static IHostBuilder configureOptionsServices(this IHostBuilder builder, string configSectionRoot)
        {
            if (!string.IsNullOrWhiteSpace(configSectionRoot))
                configSectionRoot = $"{configSectionRoot}:";
            else
                configSectionRoot = string.Empty;

            return builder.ConfigureServices((hostContext, services) =>
            {
                services.Configure<AppOptions>(hostContext.Configuration.GetSection($"{configSectionRoot}Core"));
                services.PostConfigure<AppOptions>(mo => AppOptionsPostConfigure.PostAction(mo));
                services.Configure<FileTaskOptions>(hostContext.Configuration.GetSection($"{configSectionRoot}Core:FileMan"));
            });
        }

        public static IServiceCollection AddObserverOperationFactoryExtension<T>(this IServiceCollection services)
            where T : class, IObserverOperationFactoryExtension
        {
            return services.AddSingleton<IObserverOperationFactoryExtension, T>();
        }

        public static IHostBuilder UseScanner(this IHostBuilder builder, string configSectionRoot = default)
        {
            builder.configureOptionsServices(configSectionRoot);
            builder.ConfigureServices(
                (hostContext, services) =>
                {
                    services.AddSingleton<IPathMonitorCrowd, MonitorCrowd>();
                    services.AddSingleton<IObserverProvider, ObserverCrowd>();
                    services.AddSingleton<IObserverOperationFactory, DefaultOperationFactory>();
                    services.AddObserverOperationFactoryExtension<DefaultOperationFactoryExtension>();
                    services.AddSingleton<IFileTask, FileTask>();
                });
            return builder.UseHostedService<NewFileScanHostedService>();
        }
    }
}
