using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Service.CommandService.Chat;
using Common.Models.Chat;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Chat
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ChatMessages>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public SendMessageCommandHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        public async Task<ChatMessages> Handle(SendMessageCommand request, CancellationToken cancellationToken) =>
             await _chatServiceCommand.SendMessage(request.ChatId, request.UserId, request.Message);
    }
}
