using System;

namespace Contracts.UserContract.UserResponse
{
    public class SignUpUserDataResponseDto
    {
	    public string Token { get; set; }
        public Guid UserId { get; set; }
    }
}
