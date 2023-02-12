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
    public class GetImageUser : IConsumer<GetImageUserRequestDto>
    {
	    private readonly ILogger<GetImageUser> _logger;
	    private readonly UserDbContext _context;

        public GetImageUser(
	        ILogger<GetImageUser> logger,
	        UserDbContext context)
        {
	        _logger = logger; 
	        _context = context;
        }

        public async Task Consume(ConsumeContext<GetImageUserRequestDto> context)
        {

	        var image = await _context.ImageUsers.FirstOrDefaultAsync(x =>
		        x.User.Id == context.Message.UserId.ToString()
	        );

	        await context.RespondAsync(
		        new ResultWithError<GetImageUserResponseDto>(
			        (int)HttpStatusCode.OK,
			        null,
			        new GetImageUserResponseDto()
			        {
				        ImageBaseString = image?.Image == null? null : Convert.ToBase64String(image.Image) 
			        }));
        }


    }

    public class GetImageUserDefinition : ConsumerDefinition<GetImageUser>
    {
	    public GetImageUserDefinition()
	    {
		    EndpointName = "GetImageUser";
	    }

	    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
		    IConsumerConfigurator<GetImageUser> consumerConfigurator)
	    {

	    }
    }
}
