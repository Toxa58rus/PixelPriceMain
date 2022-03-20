using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.UserContract.UserRequest;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiGateways
{
	public class UserAuthorizeFilter : IAsyncAuthorizationFilter
	{
		private readonly IClientFactory _clientFactory;

		public UserAuthorizeFilter(IClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}
		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			var request = context.HttpContext.Request;
			var token = request.Headers.Authorization;

			var requestClient = _clientFactory.CreateRequestClient<AuthenticationDataRequestDto>();

			var result = await requestClient.GetResponse<ResultWithError<bool>>(
				new AuthenticationDataRequestDto()
				{
					AccessToken = token
				});

			if (!result.Message.Result)
				context.Result = new UnauthorizedResult();
		}
	}

	public class UserAuthorizeAttribute : TypeFilterAttribute
	{
		public UserAuthorizeAttribute() : base(typeof(UserAuthorizeFilter))
		{
		
		}
	}
}
