using ChatService.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace ChatService.Services.SignalR.CommonChat
{


	public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
	{
		readonly IHttpContextAccessor _httpContextAccessor;

		public CustomAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
		{
			//	Короче для клиентов на .Net почему-то не приходит авторизация в заголовке, а на JavaScript будет в параметрах запроса
			// Implement authorization logic  
			//if (_httpContextAccessor.HttpContext.Request.Query.TryGetValue("username", out var username) &&
			//    username.Any() &&
			//    !string.IsNullOrWhiteSpace(username.FirstOrDefault()) &&
			//    username.FirstOrDefault() == "test_user")
			//{
			//	// Authorization passed  
			//	context.Succeed(requirement);
			//}
			//else
			//{
			//	// Authorization failed  
			//	context.Fail();
			//}
			context.Succeed(requirement);
			// Return completed task  
			return Task.CompletedTask;
		}
	}
	public class CustomAuthorizationRequirement : IAuthorizationRequirement
	{
		public readonly string NamePolicy;
		public CustomAuthorizationRequirement(string namePolicy)
		{
			NamePolicy = namePolicy;
		}

		
	}
}
