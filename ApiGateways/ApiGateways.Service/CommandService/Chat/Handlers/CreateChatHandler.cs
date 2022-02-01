using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.Chat;
using ApiGateways.Domain.Models.Chat;
using ApiGateways.Domain.Services.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class CreateChatHandler : HandlerBase<CreateChatCommand, ChatRooms>
    {
        private readonly IChatService _chatServiceCommand;

        public CreateChatHandler(IChatService chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<ChatRooms> Execute(CreateChatCommand request, CancellationToken cancellationToken)
        {
	       return await _chatServiceCommand.CreateChatCommand(request.CreateUserId, request.JoinUserId);
        }

    }
}
