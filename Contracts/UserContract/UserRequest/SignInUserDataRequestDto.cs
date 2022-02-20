using System;

namespace Contracts.UserContract.UserRequest
{
    public class SignInUserDataRequestDto
    {

        public string Login { get; set; }
        public string Password { get; set; }
    }
}
