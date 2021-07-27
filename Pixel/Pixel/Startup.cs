using ApiGateways.Context;
using Mail.Command.Consumers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pixel.Command;
using Pixel.Context;
using System.Reflection;

namespace Pixel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Default");
            var queryName = Configuration["RpcServer:QueryName"];

            services.AddControllers();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<PixelDbContext>(options => options.UseNpgsql(connectionString));
          
            services.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.Load("Pixel.Command"));

                x.UsingRabbitMq(
                 (context, cfg) =>
                 {

                     cfg.Host(Configuration["RabbitMQ:Host"], conf =>
                     {
                         conf.Password(Configuration["RabbitMQ:Password"]);
                         conf.Username(Configuration["RabbitMQ:UserName"]);
                     });

                     cfg.ConfigureEndpoints(context);

                 });


            });
            services.AddMassTransitHostedService();
            services.AddLogging();
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
