using Common.Rcp;
using Common.Rcp.Server;
using ImageParser.Command;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageParser
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
            var queryName = Configuration["RpcServer:QueryName"];

            services.AddControllers();
            var rpcOptions = new RpcOptions(queryName);
           // services.AddSingleton<IRpcServer, RpcServer>(s => new RpcServer(rpcOptions, new ImageParserCommandGroup()));
            services.AddLogging();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<SendMailCommand>();
                x.UsingRabbitMq(
                 (context, cfg) =>
                 {
                     cfg.Host(Configuration["RabbitMQ:Host"], conf =>
                     {
                         conf.Password(Configuration["RabbitMQ:Password"]);
                         conf.Username(Configuration["RabbitMQ:UserName"]);
                     });
                     cfg.ReceiveEndpoint("Event",
                         e => { e.ConfigureConsumer<SendMailCommand>(context); });
                 });


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

            //rpcServer.Start();

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
