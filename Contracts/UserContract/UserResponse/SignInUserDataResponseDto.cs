using System;

namespace Contracts.UserContract.UserResponse
{
    public class SignInUserDataResponseDto
    {
	    public string AccessToken { get; set; }
	    public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
    }
}
