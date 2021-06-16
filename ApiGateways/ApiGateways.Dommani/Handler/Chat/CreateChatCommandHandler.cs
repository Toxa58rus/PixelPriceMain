using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Service.CommandService.Chat;
using Common.Models.Chat;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Chat
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, ChatRooms>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public CreateChatCommandHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        public async Task<ChatRooms> Handle(CreateChatCommand request, CancellationToken cancellationToken) =>
            await _chatServiceCommand.CreateChatCommand(request.CreateUserId, request.JoinUserId);
    }
}
