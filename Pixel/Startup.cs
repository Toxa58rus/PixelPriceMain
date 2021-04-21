using ApiGateways.Context;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pixel.Dommain.Command;
using System.Reflection;
using Common.Rcp;
using Common.Rcp.Server;
using Microsoft.Extensions.Logging;

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pixel", Version = "v1" });
            });

            services.AddMediatR(typeof(GetAllPixelsCommand).GetTypeInfo().Assembly);
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

            services.AddSingleton<IRpcServer, RpcServer>(s => new RpcServer(new RpcOptions
            {
                Host = "localhost",
                QueueName = "Pixel"
            }));

            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRpcServer rpcServer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pixel v1"));
            }

            rpcServer.Start();

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
