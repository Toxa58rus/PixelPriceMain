using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Context;
using ApiGateways.Dommain;
using ApiGateways.Dommain.Command.User;
using ApiGateways.Dommain.Handler;
using ApiGateways.Service.Security;
using Common.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateways.Service.CommandService.User
{
    public class SingInHandler : HandlerBase<SingInCommand, UserToken>
    {
        private readonly ApiGatewaysDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMd5Hash _md5Hash;
        private readonly ILogger<SingInHandler> _logger;

        public SingInHandler(
            ApiGatewaysDbContext context, 
            IMd5Hash md5Hash, 
            IConfiguration configuration, 
            ILogger<SingInHandler> logger)
        {
            _context = context;
            _md5Hash = md5Hash;
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task<UserToken> Execute(SingInCommand request, CancellationToken cancellationToken)
        {
			var identity = await GetIdentity(request.Email, request.Password);

			//TODO: Обработку сделать 
			if (identity is null) throw new NotImplementedException();

			var now = DateTime.UtcNow;

			var jwt = new JwtSecurityToken(
				_configuration["AuthenticationOptions:Issuer"],
				_configuration["AuthenticationOptions:Audience"],
				notBefore: now,
				claims: identity.Claims,
				expires: now.Add(TimeSpan.FromMinutes(int.Parse(_configuration["AuthenticationOptions:LifeTime"]))),
				signingCredentials: new SigningCredentials(
					new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AuthenticationOptions:Key"])),
					SecurityAlgorithms.HmacSha256));

			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			_logger?.LogWarning($"user: {request.Email} login");

			return new UserToken(encodedJwt, identity.Name ?? string.Empty);
		}

       

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var hash = _md5Hash.GetMd5Hash(password);

            var user =
                await _context
                    .Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == hash);

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };

            var claimsIdentite =
                new ClaimsIdentity(
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentite;
        }
    }
}
