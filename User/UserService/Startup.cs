using System.Reflection;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.BL.Security;
using UserService.Context;
using UserService.Domain;

namespace UserService
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

            services.AddControllers();
            services.AddDbContext<UserDbContext>(options => options.UseNpgsql(connectionString));

            services.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.Load("UserService.BL"));
               // x.AddActivities(Assembly.Load("PixelService.Command"));
               // x.AddSagaStateMachines(Assembly.Load("PixelService.Command"));

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

            services.AddScoped<IMd5Hash, Md5Hash>();
        }

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
