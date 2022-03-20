using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.UserContract.UserRequest;
using Contracts.UserContract.UserResponse;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using UserService.Context;
using UserService.Context.Models;
using UserService.Domain;

namespace UserService.BL.Consumers.Requests
{
    public class Authentication : IConsumer<AuthenticationDataRequestDto>
    {
        private readonly UserDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMd5Hash _md5Hash;
        private readonly ILogger<Authentication> _logger;

        public Authentication(
	        UserDbContext context, 
            IMd5Hash md5Hash, 
            IConfiguration configuration, 
            ILogger<Authentication> logger)
        {
            _context = context;
            _md5Hash = md5Hash;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<AuthenticationDataRequestDto> context)
        {

	        var request = context.Message;

	        if (request.AccessToken == null)
	        {
		        await context.RespondAsync(new ResultWithError<bool>(200, null,
			        false));
		        return;
	        }

	        var now = DateTime.UtcNow;

	        try
	        {
		        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

		        var validationParameters = new TokenValidationParameters
		        {
			        ValidateIssuer = true,
			        ValidIssuer = _configuration["AuthenticationOptions:Issuer"],
			        ValidateAudience = true,
			        ValidAudience = _configuration["AuthenticationOptions:Audience"],
			        ValidateLifetime = true,
			        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AuthenticationOptions:Key"]))
				};
		        
				jwtSecurityTokenHandler.ValidateToken(request.AccessToken, validationParameters, out var validateToken);

		        await context.RespondAsync(new ResultWithError<bool>((int)HttpStatusCode.OK, null,
			        true));
	        }
	        catch (Exception ex)
	        {
		        await context.RespondAsync(new ResultWithError<bool>((int)HttpStatusCode.Unauthorized, null,
			        false));
			}
        }


    }

    public class AuthenticationDefinition : ConsumerDefinition<Authentication>
    {
	    public AuthenticationDefinition()
	    {
		    EndpointName = "AuthenticationRequest";
	    }

	    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
		    IConsumerConfigurator<Authentication> consumerConfigurator)
	    {

	    }
    }
}
