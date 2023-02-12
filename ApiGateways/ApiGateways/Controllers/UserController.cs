using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.User;

namespace ApiGateways.Controllers
{
    [ApiController]
    [UserAuthorize]
    [Route("[controller]/[action]")]
    public class UserController : BaseController
    {
	    [HttpGet]
	    public async Task<IActionResult> GetImage([FromQuery] GetImageCommand getImageCommand)
	    {
		    return await CreateResponse(async () => await Mediator.Send(getImageCommand));
	    }
		//TODO:Проверить что рабоатет получение инфы по юзеру и проверить как работает подстановка UserId если его не вписали в запрос
	    [HttpGet]
	    public async Task<IActionResult> GetUserInfo([FromQuery] GetUserInfoCommand getImageCommand)
	    {
		    return await CreateResponse(async () => await Mediator.Send(getImageCommand));
	    }
		[HttpPost]
        public async Task<IActionResult> SetImage([FromBody] SetImageCommand command) =>
	        await CreateResponse(async () => await Mediator.Send(command));
    }
}
