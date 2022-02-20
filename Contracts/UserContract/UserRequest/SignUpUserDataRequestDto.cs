using System;

namespace Contracts.UserContract.UserRequest
{
    public class SignUpUserDataRequestDto
    {
        public string ConfirmPassword { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
