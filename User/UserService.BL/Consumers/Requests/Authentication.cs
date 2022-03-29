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
	    private readonly ILogger<Authentication> _logger;
        private readonly IJwtHelper _jwtHelper;

        public Authentication(
	        ILogger<Authentication> logger,
	        IJwtHelper jwtHelper)
        {
	        _logger = logger;
            _jwtHelper = jwtHelper;
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

	        var result = _jwtHelper.ValidationSecurityToken(request.AccessToken);

	        if (result.IsValid)
	        {
		        await context.RespondAsync(new ResultWithError<bool>((int)HttpStatusCode.OK, null,
			        true));
	        }
	        else
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
