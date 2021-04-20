using ApiGateways.Common.Models.User;
using ApiGateways.Context;
using ApiGateways.Domman.Command;
using ApiGateways.Service.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ApiGateways.Domman.Handler
{
    public class SingInCommandHandler : IRequestHandler<SingInCommand, UserToken>
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMd5Hash _md5Hash;
        private readonly ILogger<SingInCommandHandler> _logger;

        public SingInCommandHandler(ApplicationDbContext context, IMd5Hash md5Hash, IConfiguration configuration, ILogger<SingInCommandHandler> logger)
        {
            _context = context;
            _md5Hash = md5Hash;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<UserToken> Handle(SingInCommand request, CancellationToken cancellationToken)
        {
            var identity = await GetIdentity(request.Email, request.Password);

            if (identity is null) return new UserToken();

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

            return new UserToken
            {
                Token = encodedJwt,
                UserId = identity.Name ?? string.Empty
            };
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
                new(ClaimsIdentity.DefaultNameClaimType, user.Id),
                new(ClaimsIdentity.DefaultNameClaimType, user.Email)
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
