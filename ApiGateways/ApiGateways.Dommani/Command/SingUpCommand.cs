using ApiGateways.Common.Models.User;
using MediatR;

namespace ApiGateways.Domman.Command
{
    public class SingUpCommand : IRequest<Users>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
