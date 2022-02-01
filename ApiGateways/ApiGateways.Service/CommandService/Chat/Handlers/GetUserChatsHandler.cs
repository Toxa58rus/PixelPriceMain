using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.Chat;
using ApiGateways.Domain.Models.Chat;
using ApiGateways.Domain.Services.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class GetUserChatsHandler : HandlerBase<GetUserChatsCommand, List<ChatRooms>>
    {
        private readonly IChatService _chatServiceCommand;

        public GetUserChatsHandler(IChatService chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<List<ChatRooms>> Execute(GetUserChatsCommand request, CancellationToken cancellationToken)
        {
	        return await _chatServiceCommand.GetChatRooms(request.UserId);
        }
    }
}
