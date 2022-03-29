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
    public class SignIn : IConsumer<SignInUserDataRequestDto>
    {
        private readonly UserDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMd5Hash _md5Hash;
        private readonly ILogger<SignIn> _logger;
        private readonly IJwtHelper _jwtHelper;

        public SignIn(
	        UserDbContext context, 
            IMd5Hash md5Hash, 
            IConfiguration configuration, 
            ILogger<SignIn> logger,
	        IJwtHelper jwtHelper)
        {
            _context = context;
            _md5Hash = md5Hash;
            _configuration = configuration;
            _logger = logger;
            _jwtHelper = jwtHelper;
        }
        public async Task Consume(ConsumeContext<SignInUserDataRequestDto> context)
        {
	        var identity = await GetIdentity(context.Message.Login, context.Message.Password);

	        if (identity == null)
	        {
		        await context.RespondAsync(new ResultWithError<SignInUserDataResponseDto>((int)HttpStatusCode.Unauthorized, null,
			        new SignInUserDataResponseDto()
			        {
				        AccessToken = null,
				        UserId = Guid.NewGuid()
			        }));
		        
		        return;
	        }

	        var jwtAccess = _jwtHelper.CreateJwtToken(identity.Claims,
		        double.Parse(_configuration["AuthenticationOptions:LifeTime"]));

	        var jwtRefresh = _jwtHelper.CreateJwtToken(identity.Claims,150);

	        _logger?.LogWarning($"user: {context.Message.Login} login");

			await context.RespondAsync(new ResultWithError<SignInUserDataResponseDto>((int)HttpStatusCode.OK, null,
		        new SignInUserDataResponseDto()
		        {
			        AccessToken = jwtAccess,
			        RefreshToken = jwtRefresh,
			        UserId = Guid.NewGuid()
		        }));

        }

     

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var hash = _md5Hash.GetMd5Hash(password);

            User user =
                await _context
                    .Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == hash);

            if (user == null) 
	            return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "TestName"),
                //new Claim(ClaimTypes.DefaultNameClaimType, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims);

            return claimsIdentity;
        }


    }

    public class SignInDefinition : ConsumerDefinition<SignIn>
    {
	    public SignInDefinition()
	    {
		    EndpointName = "SignInRequest";
	    }

	    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
		    IConsumerConfigurator<SignIn> consumerConfigurator)
	    {

	    }
    }
}
