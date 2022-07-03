using System;
using System.Threading.Tasks;
using Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateways.Controllers
{
	public class BaseController : Controller
	{
		private IMediator _mediator;

		protected IMediator Mediator
		{
			get
			{
				_mediator ??= _mediator = HttpContext.RequestServices.GetService<IMediator>();
				return _mediator;
			}
		}

		protected async Task<IActionResult> CreateResponse(Func<Task<IResultWithError>> mainLogic) 
		{
			var result = await mainLogic();
			
			if (result.IsError)
			{
				return StatusCode(result.ErrorCode, result.Message);
			}

			return new OkResult();
		}

		protected async Task<IActionResult> CreateResponse<T>(Func<Task<IResultWithError<T>>> mainLogic)
		{
			var result = await mainLogic();

			if (result.IsError)
			{
				return StatusCode(result.ErrorCode, result.Message);
			}

			return new OkObjectResult(result.Result);
		}
	}
}
