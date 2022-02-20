using ApiGateways.Context;
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
using ApiGateways.Domain;
using ApiGateways.Domain.Services.Chat;
using ApiGateways.Domain.Services.ImageParser;
using ApiGateways.Domain.Services.Mail;
using ApiGateways.Domain.Services.Pixels;
using MassTransit;
using ApiGateways.Service.CommandService.PixelService;
using ApiGateways.Service.CommandService.ImageParserService;
using ApiGateways.Service.CommandService.Mail;
using Contracts.MailContract.MailRequest;
using System.Threading.Tasks;

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
                    /*options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = (con) =>
                        {
                            return System.Threading.Tasks.Task.CompletedTask;

                        },
                        OnForbidden = (con) =>
                        {
                            return System.Threading.Tasks.Task.CompletedTask;
                        },
                        OnMessageReceived = (con) =>
                        {
                            return System.Threading.Tasks.Task.CompletedTask;
                        },
						OnChallenge = (c) =>
                        {
                            return Task.CompletedTask;
                        },
                        OnTokenValidated= (con) => { 
                        return Task.CompletedTask;
                        }

                    };*/
                    
                    
                });

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

	                cfg.Message<SendMailRequestDto>(x => x.SetEntityName("test"));
                });
                x.AddRequestClient<SendMailRequestDto>();
               
            });

            services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiGateways", Version = "v1" });
            });

            services.AddMediatR(Assembly.Load("ApiGateways.Service"));
           
            services.AddDbContext<ApiGatewaysDbContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IMd5Hash, Md5Hash>();
            services.AddScoped<IPixelAndGroupService, UserService>();
            services.AddScoped<IChatService, Service.CommandService.Chat.ChatService>();
            services.AddScoped<IImageParserService, ImageParserService>();
            services.AddScoped<IMailService, MailService>();
            //services.AddScoped<ApiGatewaysDbContext>();
            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            
           // app.UseMiddleware<ResultWithErrorMiddleware>();

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
