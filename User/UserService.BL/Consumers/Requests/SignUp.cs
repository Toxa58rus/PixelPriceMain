using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
    public class SignUp : IConsumer<SignUpUserDataRequestDto>
    {
		private readonly UserDbContext _context;
		private readonly IConfiguration _configuration;
		private readonly IMd5Hash _md5Hash;
		private readonly ILogger<SignUp> _logger;
		private readonly IJwtHelper _jwtHelper;

		public SignUp(
	        UserDbContext context,
	        IMd5Hash md5Hash,
	        IConfiguration configuration,
	        ILogger<SignUp> logger,
	        IJwtHelper jwtHelper)
        {
	        _context = context;
	        _md5Hash = md5Hash;
	        _configuration = configuration;
	        _logger = logger;
	        _jwtHelper = jwtHelper;
        }

        private User CreateUserData(SignUpUserDataRequestDto userData) =>
            new User()
            {
                Id = NewId.NextGuid().ToString(),
                Email = userData.Login.Contains("@") ? userData.Login : string.Empty,
                PasswordHash = _md5Hash.GetMd5Hash(userData.Password),
                UserName = string.Empty,
                Active = true
            };

        public async Task Consume(ConsumeContext<SignUpUserDataRequestDto> context)
        {
	        if (context.Message.Password != context.Message.ConfirmPassword)
		        return ;

	        if (_context.Users.AsNoTracking().Any(s => s.Email.Equals(context.Message.Login)))
		        return ;

	        await using var tr = await _context.Database.BeginTransactionAsync();
	        
	        var userData = CreateUserData(context.Message);
	        await _context.Users.AddAsync(userData);
	        await _context.SaveChangesAsync();

	        await tr.CommitAsync();

	        var identity = GetIdentity(userData);

	        //TODO: Обработку сделать 
	        if (identity == null)
	        {
				await context.RespondAsync(new ResultWithError<SignUpUserDataResponseDto>((int)HttpStatusCode.Unauthorized, null,
					new SignUpUserDataResponseDto()
					{
						AccessToken = null,
						UserId = Guid.NewGuid()
					}));

			}

	        var now = DateTime.UtcNow;
	        
	        var lifeTime = double.Parse(_configuration["AuthenticationOptions:LifeTime"]);

	        var jwtAccess = _jwtHelper.CreateJwtToken(identity.Claims, lifeTime);

			var jwtRefresh = _jwtHelper.CreateJwtToken(identity.Claims, 150);


			await context.RespondAsync(new ResultWithError<SignUpUserDataResponseDto>((int)HttpStatusCode.OK, null,
		        new SignUpUserDataResponseDto()
		        {
			        AccessToken = jwtAccess,
					RefreshToken= jwtRefresh,
					UserId = Guid.Parse(userData.Id)
				}));
			
			//await _publish.Publish(new CreateUserEvent { Userid = Guid.Parse(userData.Id), MailAddress = userData.Email }, cancellationToken);

			_logger.LogInformation($"user: {userData.Id}, {userData.Email} has registered");
        }
        private ClaimsIdentity GetIdentity(User user)
        {
	        if (user == null)
		        return null;

	        var claims = new List<Claim>
	        {
		        new Claim(ClaimTypes.Email,user.Email)
	        };

	        
	        return new ClaimsIdentity(claims); 
        }
	}
   
	public class SignUpDefinition : ConsumerDefinition<SignUp>
    {
	    public SignUpDefinition()
	    {
		    EndpointName = "SignUpRequest";
	    }

	    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
		    IConsumerConfigurator<SignUp> consumerConfigurator)
	    {

	    }
    }
}
