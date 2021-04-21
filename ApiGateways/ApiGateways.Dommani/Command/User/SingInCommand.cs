using Common.Models.User;
using MediatR;

namespace ApiGateways.Dommain.Command.User
{
    public class SingInCommand : IRequest<UserToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
