using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Chat;
using Common.Models.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class CreateChatHandler : HandlerBase<CreateChatCommand, ChatRooms>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public CreateChatHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<ChatRooms> Execute(CreateChatCommand request, CancellationToken cancellationToken)
        {
	       return await _chatServiceCommand.CreateChatCommand(request.CreateUserId, request.JoinUserId);
        }

    }
}
