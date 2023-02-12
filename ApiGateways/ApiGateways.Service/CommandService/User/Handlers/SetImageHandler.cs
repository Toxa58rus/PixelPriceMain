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
	public class SetImageHandler : HandlerBase<SetImageCommand, IResultWithError>//ResultWithError<SignUpDataResponse>>
	{
		private readonly IUserService _userService;
		private readonly ILogger<SetImageHandler> _logger;

		public SetImageHandler(
			IUserService userService,
			ILogger<SetImageHandler> logger)
		{
			_userService = userService;
			_logger = logger;

		}

		protected override async Task<IResultWithError> Execute(SetImageCommand request, CancellationToken cancellationToken)
		{
			return await _userService.SetImage(request);
		}
	}
}
