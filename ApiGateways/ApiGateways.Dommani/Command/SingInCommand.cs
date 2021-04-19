using ApiGateways.Common.Models.User;
using MediatR;

namespace ApiGateways.Domman.Command
{
    public class SingInCommand : IRequest<UserToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
