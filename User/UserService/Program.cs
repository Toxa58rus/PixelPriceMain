using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using UserService.Context;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace UserService
{
    public class Program
    {
        public static void Main(string[] args)
        {
	        var host = CreateHostBuilder(args).Build();
			
	        using (var scope = host.Services.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
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
