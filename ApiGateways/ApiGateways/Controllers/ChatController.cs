using ApiGateways.Dommain.Command.Chat;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiGateways.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]/[action]")]
    public class ChatController : BaseController
    {
        [HttpPost]
        public async Task<string> CreateUserChat() => await Mediator.Send(new CreateChatCommand());
    }
}
