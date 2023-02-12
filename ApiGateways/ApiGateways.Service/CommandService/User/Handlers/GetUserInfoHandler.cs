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
	public class GetUserInfoHandler : HandlerBase<GetUserInfoCommand, IResultWithError<GetUserInfoResponse>>
	{
		private readonly IUserService _userService;
		private readonly ILogger<GetUserInfoHandler> _logger;

		public GetUserInfoHandler(
			IUserService userService,
			ILogger<GetUserInfoHandler> logger)
		{
			_userService = userService;
			_logger = logger;

		}

		protected override async Task<IResultWithError<GetUserInfoResponse>> Execute(GetUserInfoCommand request, CancellationToken cancellationToken)
		{
			return await _userService.GetUserInfo(request);
		}
	}
}
