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
using UserService.BL.Security;
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
	        var request = context.Message;

	        JwtInfoUser jwtInfoUser = null;
	        if (request.RefreshToken != null)
	        {
		        jwtInfoUser = await GetIdentity(request.RefreshToken);
		        
		        _logger?.LogWarning($"user: {request.RefreshToken} refreshToken");
			}
	        else
	        {
		        jwtInfoUser = await GetIdentity(request.Login, request.Password);

		        _logger?.LogWarning($"user: {request.Login} login , {request.Password} password");
			}

	        if (jwtInfoUser == null)
	        {
		        await context.RespondAsync(new ResultWithError<SignInUserDataResponseDto>(
			        (int)HttpStatusCode.Unauthorized, null,
			        null));

		        return;
	        }

	        await context.RespondAsync(new ResultWithError<SignInUserDataResponseDto>((int)HttpStatusCode.OK, null,
		        new SignInUserDataResponseDto()
		        {
			        AccessToken = jwtInfoUser.AccessToken,
			        RefreshToken = jwtInfoUser.RefreshToken,
			        UserId = jwtInfoUser.UserId
		        }));
        }

        private async Task<JwtInfoUser> GetIdentity(string refreshToken)
        {
	        var valid = _jwtHelper.ValidationSecurityToken(refreshToken);

	        if (valid.IsValid)
	        {
		        var email = valid.ClaimsPrincipal.FindFirstValue(ClaimTypes.Email);

		        var user = await _context
			        .Users
			        .AsNoTracking()
			        .FirstOrDefaultAsync(x => x.Email == email);

		        if (user == null)
			        return null;

		        var claims = new List<Claim>
		        {
			        new Claim(ClaimTypes.Email, email),
		        };

		        var claimsIdentity = new ClaimsIdentity(claims);

		        var jwtAccess = _jwtHelper.CreateJwtToken(claimsIdentity.Claims,
			        double.Parse(_configuration["AuthenticationOptions:LifeTime"]));

		        var jwtRefresh = _jwtHelper.CreateJwtToken(claimsIdentity.Claims, 150);
		        return new JwtInfoUser()
		        {
			        AccessToken = jwtAccess,
			        RefreshToken = jwtRefresh,
			        UserId = Guid.Parse(user.Id)
		        };

	        }

	        return null;
        }

        private async Task<JwtInfoUser> GetIdentity(string email, string password)
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
		        new Claim(ClaimTypes.Email, email),
	        };

	        var claimsIdentity = new ClaimsIdentity(claims);

	        var jwtAccess = _jwtHelper.CreateJwtToken(claimsIdentity.Claims,
		        double.Parse(_configuration["AuthenticationOptions:LifeTime"]));

	        var jwtRefresh = _jwtHelper.CreateJwtToken(claimsIdentity.Claims, 150);

	        return new JwtInfoUser()
	        {
		        AccessToken = jwtAccess,
		        RefreshToken = jwtRefresh,
		        UserId = Guid.Parse(user.Id)
	        };
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
