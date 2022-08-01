using ApiGateways.Context;
using ApiGateways.Domain.Services.Chat;
using ApiGateways.Domain.Services.ImageParser;
using ApiGateways.Domain.Services.Mail;
using ApiGateways.Domain.Services.Pixels;
using ApiGateways.Domain.Services.User;
using ApiGateways.Service.CommandService.ImageParserService;
using ApiGateways.Service.CommandService.Mail;
using ApiGateways.Service.CommandService.PixelService;
using ApiGateways.Service.CommandService.User;
using Contracts.MailContract.MailRequest;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ApiGateways.Domain.Services;

namespace ApiGateways
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Default");
            services.AddMassTransit(x =>
            {
               
                x.UsingRabbitMq(
                (context, cfg) =>
                {
	                cfg.Host(Configuration["RabbitMQ:Host"], 5672, "/", conf =>
	                {
		                conf.Password(Configuration["RabbitMQ:Password"]);
		                conf.Username(Configuration["RabbitMQ:UserName"]);
                    });

	                //cfg.Message<SendMailRequestDto>(x => x.SetEntityName("test"));
                });
                //x.AddRequestClient<SendMailRequestDto>();
               
            });

            services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiGateways", Version = "v1" });
            });


            services.AddMediatR(Assembly.Load("ApiGateways.Service"));
           
            services.AddDbContext<ApiGatewaysDbContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IPixelAndGroupService, PixelService>();
            services.AddScoped<IChatService, Service.CommandService.Chat.ChatService>();
            services.AddScoped<IImageParserService, ImageParserService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserContext>();
            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiGateways v1"));
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
