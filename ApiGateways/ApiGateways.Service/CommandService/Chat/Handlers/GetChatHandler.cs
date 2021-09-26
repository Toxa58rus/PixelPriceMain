using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Chat;
using Common.Models.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class GetChatHandler : HandlerBase<GetChatCommand, ChatRooms> 
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public GetChatHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<ChatRooms> Execute(GetChatCommand request, CancellationToken cancellationToken)
        {
	        return await _chatServiceCommand.GetChat(request.RoomId);
        }

    }
}

