using ApiGateways.Domain.Models.User.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.User
{
    public class SingInCommand : IRequest<IResultWithError<SignInDataResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken{ get; set; }
    }
}
