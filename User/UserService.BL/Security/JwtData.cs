using System;
using System.Security.Cryptography;
using System.Text;
using UserService.Domain;

namespace UserService.BL.Security
{
    public class JwtInfoUser
    {
	    public string AccessToken { get; set; }
	    public string RefreshToken { get; set; }
	    public Guid UserId { get; set; }
	}
}
