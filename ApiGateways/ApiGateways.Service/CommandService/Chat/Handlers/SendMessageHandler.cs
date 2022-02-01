using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.Chat;
using ApiGateways.Domain.Models.Chat;
using ApiGateways.Domain.Services.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class SendMessageHandler : HandlerBase<SendMessageCommand, ChatMessages>
    {
        private readonly IChatService _chatServiceCommand;

        public SendMessageHandler(IChatService chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<ChatMessages> Execute(SendMessageCommand request, CancellationToken cancellationToken)
        {
	        return await _chatServiceCommand.SendMessage(request.ChatId, request.UserId, request.Message);
        }
    }
}
