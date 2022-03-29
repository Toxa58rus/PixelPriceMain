using System.Security.Claims;

namespace UserService.Domain;

public interface IJwtHelper
{
	public string CreateJwtToken(IEnumerable<Claim> claims, double lifeTimeInMinutes);
	public JwtData ValidationSecurityToken(string token);
}