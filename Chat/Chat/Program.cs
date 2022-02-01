using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System.Reflection;
using ChatService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace ChatService
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var host = CreateHostBuilder(args).Build();
			using (var scope = host.Services.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
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
