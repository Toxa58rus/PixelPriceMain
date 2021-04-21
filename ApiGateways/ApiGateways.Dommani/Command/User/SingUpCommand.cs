using Common.Models.User;
using MediatR;

namespace ApiGateways.Dommain.Command.User
{
    public class SingUpCommand : IRequest<Users>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
