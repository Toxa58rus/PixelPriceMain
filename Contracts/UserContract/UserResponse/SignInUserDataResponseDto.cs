using System;

namespace Contracts.UserContract.UserResponse
{
    public class SignInUserDataResponseDto
    {
	    public string Token { get; set; }
        public Guid UserId { get; set; }
    }
}
