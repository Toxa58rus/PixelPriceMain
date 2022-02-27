using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.User;
using ApiGateways.Domain.Models.User.Response;
using ApiGateways.Domain.Services.User;
using Common.Errors;
using Microsoft.Extensions.Logging;

namespace ApiGateways.Service.CommandService.User.Handlers
{
    public class SingInHandler : HandlerBase<SingInCommand, ResultWithError<SignInDataResponse>>
    {
        private readonly IUserService _userService;
        private readonly ILogger<SingInHandler> _logger;

        public SingInHandler(
            IUserService userService,
            ILogger<SingInHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        protected override async Task<ResultWithError<SignInDataResponse>> Execute(SingInCommand request, CancellationToken cancellationToken)
        {
	        return await _userService.SignIn(request);
        }
    }
}
