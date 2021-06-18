using ApiGateways.Dommain.Command.Chat;
using Common.Models.Chat;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateways.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]/[action]")]
    public class ChatController : BaseController
    {
        [HttpPost]
        public async Task<ChatRooms> GetUserChat([FromBody] GetChatCommand command) => 
            await Mediator.Send(command);

        [HttpPost]
        public async Task<List<ChatMessages>> GetMessages([FromBody] GetMessagesCommand command) =>
           await Mediator.Send(command);

        [HttpPost]
        public async Task<ChatMessages> SendMessage([FromBody] SendMessageCommand command) =>
          await Mediator.Send(command);

        [HttpPost]
        public async Task<ChatMessages> EditMessage([FromBody] EditMessageCommand command) =>
          await Mediator.Send(command);

        [HttpDelete]
        public async Task<bool> DeleteMessage([FromBody] DeleteMessageCommand command) =>
          await Mediator.Send(command);

        [HttpPost]
        public async Task<ChatRooms> CreateChat([FromBody] CreateChatCommand command) =>
          await Mediator.Send(command);

        [HttpPost]
        public async Task<List<ChatRooms>> GetUserChats([FromBody] GetUserChatsCommand command) =>
        await Mediator.Send(command);
    }
}
