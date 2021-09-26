using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Chat;
using Common.Models.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class SendMessageHandler : HandlerBase<SendMessageCommand, ChatMessages>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public SendMessageHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<ChatMessages> Execute(SendMessageCommand request, CancellationToken cancellationToken)
        {
	        return await _chatServiceCommand.SendMessage(request.ChatId, request.UserId, request.Message);
        }
    }
}
