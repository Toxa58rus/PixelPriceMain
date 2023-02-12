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
	public class GetImageHandler : HandlerBase<GetImageCommand, IResultWithError<string>>
	{
		private readonly IUserService _userService;
		private readonly ILogger<GetImageHandler> _logger;

		public GetImageHandler(
			IUserService userService,
			ILogger<GetImageHandler> logger)
		{
			_userService = userService;
			_logger = logger;

		}

		protected override async Task<IResultWithError<string>> Execute(GetImageCommand request, CancellationToken cancellationToken)
		{
			return await _userService.GetImage(request);
		}
	}
}
