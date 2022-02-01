using System.Reflection;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageParserService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddControllers();
	        
	        services.AddLogging();
	        
	        services.AddMassTransit(x =>
	        {
		        x.UsingRabbitMq(
			        (context, cfg) =>
			        {
				        cfg.Host(Configuration["RabbitMQ:Host"], conf =>
				        {
					        conf.Password(Configuration["RabbitMQ:Password"]);
					        conf.Username(Configuration["RabbitMQ:UserName"]);
				        });
			        });

	            x.AddConsumers(Assembly.Load("ImageParserService.Command"));

            });
	        

            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
