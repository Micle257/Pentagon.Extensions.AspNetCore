using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Pentagon.AspNetCore.Demo
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using Serilog;

    public class Program
    {
        static IHost _host;

        static ILogger LogStartup => Log.ForContext<Program>();

        public static async Task Main(string[] args)
        {
            try
            {
                try
                {
                    _host = CreateHostBuilder(args: args).Build();
                }
                catch (Exception e)
                {
                    LogStartup.Fatal(e, "Application crashed during host build.");
                    throw;
                }

                Debug.Assert(_host != null, nameof(_host) + " != null");

                LogStartup.Information("Main init");
                //TODO LogStartup.Verbose("OS Environment: {@Environment}", OSEnvironment.Instance);

                try
                {
                    await _host.RunAsync().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    LogStartup.Fatal(e, "Application crashed during host run.");
                    throw;
                }
            }
            finally
            {
                // ensure disposed serilog logger
                Log.CloseAndFlush();
            }
        }

        static IHostBuilder CreateHostBuilder([NotNull] string[] args) =>
                Host.CreateDefaultBuilder(args: args)
                    .UseApplicationLogging(runnerName: "Blazor")
                    .UseFileVaultBlazorApplication();
    }
}
