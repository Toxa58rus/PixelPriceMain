using ApiGateways.Context;
using ApiGateways.Dommain.Command.User;
using ApiGateways.Service.Security;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using ApiGateways.Dommain;
using ApiGateways.Dommain.Handler.Chat;
using ApiGateways.Dommain.Handler.ImageParser;
using ApiGateways.Dommain.Handler.Mail;
using ApiGateways.Dommain.Handler.Pixels;
using MassTransit;
using ApiGateways.Service.CommandService.PixelService;
using ApiGateways.Service.CommandService.ImageParserService;
using ApiGateways.Service.CommandService.Mail;
using Contracts.MailContract.MailRequest;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; // ssl
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Configuration["AuthenticationOptions:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = Configuration["AuthenticationOptions:Audience"],
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["AuthenticationOptions:Key"])),
                            ValidateIssuerSigningKey = true
                        };
                });

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
                x.AddRequestClient<SendMailRequest>();
               
            });

            services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiGateways", Version = "v1" });
            });

            services.AddMediatR(typeof(SingUpCommand).GetTypeInfo().Assembly);
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApiGatewaysDbContext>(options => options.UseNpgsql(connectionString));

            services.AddTransient<IMd5Hash, Md5Hash>();
            services.AddTransient<IPixelServiceCommand, PixelService>();
            services.AddTransient<IChatServiceCommand, Service.CommandService.Chat.ChatService>();
            services.AddTransient<IImageParserServiceCommand, ImageParserService>();
            services.AddTransient<IMailServiceCommand, MailService>();
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

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
