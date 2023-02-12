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
    public class GetUserInfo : IConsumer<GetUserInfoRequestDto>
    {
	    private readonly ILogger<GetUserInfo> _logger;
	    private readonly UserDbContext _context;

        public GetUserInfo(
	        ILogger<GetUserInfo> logger,
	        UserDbContext context)
        {
	        _logger = logger; 
	        _context = context;
        }

        public async Task Consume(ConsumeContext<GetUserInfoRequestDto> context)
        {

	        var user = await _context.Users.FirstOrDefaultAsync(x =>
		        x.Id == context.Message.UserId.ToString());

	        if (user == null)
	        { 
		        await context.RespondAsync(
			        new ResultWithError<GetUserInfoResponseDto>(
				        (int)HttpStatusCode.BadRequest,
				        "Пользоватлеь не найден",
				        null));
		        return;
	        }

			await context.RespondAsync(
		        new ResultWithError<GetUserInfoResponseDto>(
			        (int)HttpStatusCode.OK,
			        null,
			        new GetUserInfoResponseDto()
			        {
						Name=user.UserName
			        }));
        }
    }

    public class GetUserInfoDefinition : ConsumerDefinition<GetUserInfo>
    {
	    public GetUserInfoDefinition()
	    {
		    EndpointName = "GetUserInfo";
	    }

	    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
		    IConsumerConfigurator<GetUserInfo> consumerConfigurator)
	    {

	    }
    }
}
