using ApiGateways.Context;
using Chat.Command;
using Common.Rcp;
using Common.Rcp.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Chat
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
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApiGatewaysDbContext>(options => options.UseNpgsql(connectionString));

            var rpcOptions = new RpcOptions("Chat");
            services.AddSingleton<IRpcServer, RpcServer>(s => new RpcServer(rpcOptions, new ChatCommandGroup()));
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRpcServer rpcServer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
