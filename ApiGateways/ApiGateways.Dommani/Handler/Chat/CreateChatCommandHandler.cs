using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Service.CommandService.Chat;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Chat
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, string>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public CreateChatCommandHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        public async Task<string> Handle(CreateChatCommand request, CancellationToken cancellationToken) =>
            await _chatServiceCommand.CreateChatCommand();
    }
}
