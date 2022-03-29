using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserService.Domain;

namespace UserService.BL.Security
{
	public class JwtHelper : IJwtHelper
	{
		private readonly IConfiguration _configuration;
		public JwtHelper(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string CreateJwtToken(IEnumerable<Claim> claims, double lifeTimeInMinutes)
		{

			var now = DateTime.UtcNow;

			var tokenHandler = new JwtSecurityTokenHandler();

			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AuthenticationOptions:Key"])),
				SecurityAlgorithms.HmacSha256);

			var jwt = new JwtSecurityToken(
				_configuration["AuthenticationOptions:Issuer"],
				_configuration["AuthenticationOptions:Audience"],
				notBefore: now,
				claims: claims,
				expires: now.Add(TimeSpan.FromMinutes(lifeTimeInMinutes)),
				signingCredentials: signingCredentials
				);

			return tokenHandler.WriteToken(jwt);

		}

		public JwtData ValidationSecurityToken(string token)
		{
			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

			var validationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = true,
				ValidIssuer = _configuration["AuthenticationOptions:Issuer"],
				ValidateAudience = true,
				ValidAudience = _configuration["AuthenticationOptions:Audience"],
				ValidateLifetime = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AuthenticationOptions:Key"]))
			};

			bool isValid = true;

			ClaimsPrincipal claimsPrincipal = null;
			
			SecurityToken securityToken = null;
			
			JwtData jwtData = null;
			
			try
			{
				claimsPrincipal =
					jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out securityToken);
			}
			catch
			{
				isValid = false;
			}
			finally
			{
				jwtData = new JwtData(claimsPrincipal, securityToken, isValid);
			}

			return jwtData;
		}
	}
}
