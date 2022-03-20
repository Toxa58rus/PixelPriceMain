﻿using System;
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

        public SignIn(
	        UserDbContext context, 
            IMd5Hash md5Hash, 
            IConfiguration configuration, 
            ILogger<SignIn> logger)
        {
            _context = context;
            _md5Hash = md5Hash;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<SignInUserDataRequestDto> context)
        {
	        var identity = await GetIdentity(context.Message.Login, context.Message.Password);

	        //TODO: Обработку сделать 
	        if (identity == null)
	        {
		        await context.RespondAsync(new ResultWithError<SignInUserDataResponseDto>((int)HttpStatusCode.Unauthorized, null,
			        new SignInUserDataResponseDto()
			        {
				        Token = null,
				        UserId = Guid.NewGuid()
			        }));
		        
		        return;
	        }

	        var now = DateTime.UtcNow;

	        var jwt = new JwtSecurityToken(
		        _configuration["AuthenticationOptions:Issuer"],
		        _configuration["AuthenticationOptions:Audience"],
		        notBefore: now,
		        claims: identity.Claims,
		        expires: now.Add(TimeSpan.FromMinutes(double.Parse(_configuration["AuthenticationOptions:LifeTime"]))),
		        signingCredentials: new SigningCredentials(
			        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AuthenticationOptions:Key"])),
			        SecurityAlgorithms.HmacSha256));

	        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

	        _logger?.LogWarning($"user: {context.Message.Login} login");

	        await context.RespondAsync(new ResultWithError<SignInUserDataResponseDto>((int)HttpStatusCode.OK, null,
		        new SignInUserDataResponseDto()
		        {
			        Token = encodedJwt,
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