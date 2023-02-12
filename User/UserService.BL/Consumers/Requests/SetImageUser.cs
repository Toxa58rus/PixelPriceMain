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
    public class SetImageUser : IConsumer<SetImageUserRequestDto>
    {
	    private readonly ILogger<SetImageUser> _logger;
	    private readonly UserDbContext _context;

        public SetImageUser(
	        ILogger<SetImageUser> logger,
	        UserDbContext context)
        {
	        _logger = logger; 
	        _context = context;
        }

        public async Task Consume(ConsumeContext<SetImageUserRequestDto> context)
        {

	        var user = await _context.Users
		        .Include(y => y.ImageUser)
		        .FirstAsync(x => x.Id == context.Message.UserId.ToString());

	        if (user.ImageUser == null)
	        {
		        user.ImageUser = new ImageUser()
		        {
			        Image = Convert.FromBase64String(context.Message.ImageBaseString)
		        };
	        }
	        else
	        {
		        user.ImageUser.Image = Convert.FromBase64String(context.Message.ImageBaseString);
	        }

	        _context.Update(user);
			
	        await _context.SaveChangesAsync();

			await context.RespondAsync(
				new ResultWithError(
					(int)HttpStatusCode.OK,
					null
				));

        }


    }

    public class SetImageUserDefinition : ConsumerDefinition<SetImageUser>
    {
	    public SetImageUserDefinition()
	    {
		    EndpointName = "SetImageUser";
	    }

	    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
		    IConsumerConfigurator<SetImageUser> consumerConfigurator)
	    {

	    }
    }
}
