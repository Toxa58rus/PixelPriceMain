using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.Chat;
using ApiGateways.Domain.Models.Chat;
using ApiGateways.Domain.Services.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class GetChatHandler : HandlerBase<GetChatCommand, ChatRooms> 
    {
        private readonly IChatService _chatServiceCommand;

        public GetChatHandler(IChatService chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<ChatRooms> Execute(GetChatCommand request, CancellationToken cancellationToken)
        {
	        return await _chatServiceCommand.GetChat(request.RoomId);
        }

    }
}

