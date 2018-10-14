using System;
using System.Collections.Generic;
using System.IO;
using Archiving.Monitor;
using Archiving.Observer;
using Newtonsoft.Json;
using System.Linq;
using Archiving.Entity;
using Archiving.Operation;
using Archiving.Operation.Net;
using Archiving;
using Suites.Core.Process;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Archiving.Operation.FileExt.Core;
using Archiving.Core.Options;
using Archiving.Operation.Compose;
using NLog.Extensions.Logging;
using NLog;

namespace Scanner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (ProcessNotSingletonException pnse)
            {
                logger.Error(pnse, "process not singleton.");
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            HostBuilderExt.CreateDefaultHostBuilder(args)
                .ForceProcessSingleton()
                .UseScanner()
                .UseConsoleLifetime();
    }
}
