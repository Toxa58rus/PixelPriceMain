using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.UserContract.UserRequest;
using Contracts.UserContract.UserResponse;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Context;
using UserService.Domain;

namespace UserService.BL.Consumers.Requests
{
    public class Authentication : IConsumer<AuthenticationDataRequestDto>
    {
	    private readonly ILogger<Authentication> _logger;
        private readonly IJwtHelper _jwtHelper;
        private readonly UserDbContext _context;

        public Authentication(
	        ILogger<Authentication> logger,
	        IJwtHelper jwtHelper,
	        UserDbContext context)
        {
	        _logger = logger;
            _jwtHelper = jwtHelper;
            _context = context;
        }

        public async Task Consume(ConsumeContext<AuthenticationDataRequestDto> context)
        {

	        var request = context.Message;

	        if (string.IsNullOrEmpty(request.AccessToken))
	        {
		        await context.RespondAsync(new ResultWithError<AuthenticationDataResponseDto>((int)HttpStatusCode.Unauthorized, null,
			        new AuthenticationDataResponseDto()
			        {
				        UserId = Guid.Empty
			        }));
		        return;
	        }

	        var result = _jwtHelper.ValidationSecurityToken(request.AccessToken);

	        if (!result.IsValid)
	        {
		        await context.RespondAsync(new ResultWithError<AuthenticationDataResponseDto>((int)HttpStatusCode.Unauthorized, null,
			        new AuthenticationDataResponseDto()
			        {
				        UserId = Guid.Empty
			        }));
		        return;
			}

	        var email = result.ClaimsPrincipal.FindFirstValue(ClaimTypes.Email);
	        
	        if (email == null)
	        {
		        await context.RespondAsync(
			        new ResultWithError<AuthenticationDataResponseDto>(
				        (int)HttpStatusCode.Unauthorized, 
				        null, 
				        null));
		        return;
			}

	        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

	        if (user == null)
			{
				await context.RespondAsync(
					new ResultWithError<AuthenticationDataResponseDto>(
						(int)HttpStatusCode.Unauthorized,
						null,
						null));
				return;
			}

	        if (result.IsValid)
	        {
				await context.RespondAsync(
					new ResultWithError<AuthenticationDataResponseDto>(
						(int)HttpStatusCode.OK,
						null,
						new AuthenticationDataResponseDto()
						{
							UserId = Guid.Parse(user.Id)
						}));
			}
	        else
	        {
		        await context.RespondAsync(new ResultWithError<AuthenticationDataResponseDto>((int)HttpStatusCode.Unauthorized, null,
			        null));
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
