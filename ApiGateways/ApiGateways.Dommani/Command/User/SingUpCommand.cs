﻿using MediatR;

namespace ApiGateways.Domain.Command.User
{
    public class SingUpCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
