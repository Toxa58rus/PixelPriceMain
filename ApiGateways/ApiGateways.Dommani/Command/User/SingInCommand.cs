using ApiGateways.Domain.Models.User;
using MediatR;

namespace ApiGateways.Domain.Command.User
{
    public class SingInCommand : IRequest<UserToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
