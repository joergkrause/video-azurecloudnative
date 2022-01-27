using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Heise.Workshop.Course.ProcessIsolationFunction
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureAppConfiguration(config => config
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("local.settings.json")
                    .AddEnvironmentVariables())
                .ConfigureServices(services =>
                {
                    services.AddSingleton(sp => new MySpecialService());
                })
                .Build();

            host.Run();
        }
    }
}
