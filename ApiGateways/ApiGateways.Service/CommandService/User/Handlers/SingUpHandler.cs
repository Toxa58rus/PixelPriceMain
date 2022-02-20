using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Context;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.User;
using ApiGateways.Domain.Models.User.Response;
using ApiGateways.Domain.Services.Pixels;
using ApiGateways.Domain.Services.User;
using Common.Errors;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ApiGateways.Service.CommandService.User.Handlers
{
	public class SingUpHandler : HandlerBase<SingUpCommand, ResultWithError<SignUpDataResponse>>
	{
		private readonly IUserService _userService;
		private readonly ILogger<SingUpHandler> _logger;

		public SingUpHandler(
			IUserService userService,
			ILogger<SingUpHandler> logger)
		{
			_userService = userService;
			_logger = logger;

		}

		protected override async Task<ResultWithError<SignUpDataResponse>> Execute(SingUpCommand request, CancellationToken cancellationToken)
		{
			return await _userService.SignUp(request.Email, request.Password);
		}
	}
}
