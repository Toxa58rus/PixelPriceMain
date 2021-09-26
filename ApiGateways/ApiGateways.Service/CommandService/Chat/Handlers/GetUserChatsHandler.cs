using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Chat;
using Common.Models.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class GetUserChatsHandler : HandlerBase<GetUserChatsCommand, List<ChatRooms>>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public GetUserChatsHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<List<ChatRooms>> Execute(GetUserChatsCommand request, CancellationToken cancellationToken)
        {
	        return await _chatServiceCommand.GetChatRooms(request.UserId);
        }
    }
}
