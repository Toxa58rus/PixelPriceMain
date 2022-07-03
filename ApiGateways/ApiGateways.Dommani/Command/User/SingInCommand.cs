using ApiGateways.Domain.Models.User.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.User
{
    public class SingInCommand : IRequest<IResultWithError<SignInDataResponse>>//IRequest<ResultWithError<SignInDataResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
