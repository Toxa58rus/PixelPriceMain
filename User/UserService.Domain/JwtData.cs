using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace UserService.Domain
{
	public class JwtData
	{
		public ClaimsPrincipal ClaimsPrincipal { get;}
		public SecurityToken SecurityToken { get; }
		public bool IsValid { get; }

		public JwtData(
			ClaimsPrincipal claimsPrincipal, 
			SecurityToken securityToken,
			bool isValid)
		{
			ClaimsPrincipal = claimsPrincipal;
			SecurityToken = securityToken;
			IsValid = isValid;
		}
	}
}
