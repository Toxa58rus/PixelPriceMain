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

		public SignUp(
	        UserDbContext context,
	        IMd5Hash md5Hash,
	        IConfiguration configuration,
	        ILogger<SignUp> logger)
        {
	        _context = context;
	        _md5Hash = md5Hash;
	        _configuration = configuration;
	        _logger = logger;

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

	        var identity = await GetIdentity(context.Message.Login, context.Message.Password);

	        //TODO: Обработку сделать 
	        if (identity is null)
	        {
		        await context.RespondAsync(new ResultWithError<SignInUserDataResponseDto>((int)HttpStatusCode.Unauthorized, null,
			        new SignInUserDataResponseDto()
			        {
				        Token = null,
				        UserId = Guid.NewGuid()
			        }));

	        }

	        var now = DateTime.UtcNow;
	        var lifeTime = double.Parse(_configuration["AuthenticationOptions:LifeTime"]);

			var jwt = new JwtSecurityToken(
		        _configuration["AuthenticationOptions:Issuer"],
		        _configuration["AuthenticationOptions:Audience"],
		        notBefore: now,
		        claims: identity.Claims,
		        expires: now.Add(TimeSpan.FromMinutes(lifeTime)),
		        signingCredentials: new SigningCredentials(
			        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AuthenticationOptions:Key"])),
			        SecurityAlgorithms.HmacSha256));

	        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			await context.RespondAsync(new ResultWithError<SignUpUserDataResponseDto>((int)HttpStatusCode.OK, null,
		        new SignUpUserDataResponseDto()
		        {
			        Token = encodedJwt,
			        UserId = Guid.Parse(userData.Id)
				}));
			
			//await _publish.Publish(new CreateUserEvent { Userid = Guid.Parse(userData.Id), MailAddress = userData.Email }, cancellationToken);

			_logger.LogInformation($"user: {userData.Id}, {userData.Email} has registered");
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
		        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id),
		        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
	        };

	        var claimsIdentity =
		        new ClaimsIdentity(
			        claims,
			        "Token",
			        ClaimsIdentity.DefaultNameClaimType,
			        ClaimsIdentity.DefaultRoleClaimType);

	        return claimsIdentity;
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
