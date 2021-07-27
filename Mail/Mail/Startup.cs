using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mail.Command;
using Microsoft.Extensions.Options;
using MassTransit;
using Mail.Context;
using Mail.Command.Consumers;
using System.Reflection;

namespace Mail
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<MailDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Default")));
           
            services.AddLogging();

            services.Configure<MailConfig>(Configuration.GetSection("SettingSMTP"));
          
            services.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.Load("Mail.Command"));

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

            services.AddHttpClient("smtpMail", (serv,conf) =>
            {
                var mailConf = serv.GetService<IOptions<MailConfig>>().Value;
                conf.BaseAddress = new Uri(mailConf.BaseAddress);
                conf.DefaultRequestHeaders.Add("authorization", mailConf.ApiKey);
                conf.DefaultRequestHeaders.Add("content-type", "multipart/form-data");

            });

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
