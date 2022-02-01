using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;
using System.Reflection;
using ApiGateways.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace ApiGateways
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
	            var db = scope.ServiceProvider.GetRequiredService<ApiGatewaysDbContext>();
	            db.Database.Migrate();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var serviceName = Assembly.GetEntryAssembly()?.GetName().Name;
            MappedDiagnosticsLogicalContext.Set("Service", serviceName);

            return 
                Host.CreateDefaultBuilder(args)
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                    .UseNLog()
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
	                    webBuilder.UseStartup<Startup>();
                    });
        }
    }
}
