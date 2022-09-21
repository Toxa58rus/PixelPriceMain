
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApiGateways.Domain.Services;
using ApiGateways.Service;
using Common.Errors;
using Contracts.UserContract.UserRequest;
using Contracts.UserContract.UserResponse;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiGateways
{
	public class UserAuthorizeFilter : IAsyncAuthorizationFilter
	{
		private readonly IClientFactory _clientFactory;
		private readonly UserContext _userContext;


		public UserAuthorizeFilter(IClientFactory clientFactory, UserContext userContext)
		{
			_clientFactory = clientFactory;
			_userContext = userContext;
		}
		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			var request = context.HttpContext.Request;
			var token = request.Headers.Authorization;

			var requestClient = _clientFactory.CreateRequestClient<AuthenticationDataRequestDto>();

			var result = await requestClient.GetResponse<ResultWithError<AuthenticationDataResponseDto>>(
				new AuthenticationDataRequestDto()
				{
					AccessToken = token
				});

			if (result.Message.ErrorCode == (int)HttpStatusCode.BadRequest)
			{

				context.Result = new BadRequestResult();
				return;
			}

			if (result.Message.ErrorCode == (int)HttpStatusCode.Unauthorized)
			{

				context.Result = new UnauthorizedResult();
				return;
			}

			_userContext.UserId = result.Message.Result.UserId;
		}
	}

	public class UserAuthorizeAttribute : TypeFilterAttribute
	{
		public UserAuthorizeAttribute() : base(typeof(UserAuthorizeFilter))
		{
		
		}
	}
}
