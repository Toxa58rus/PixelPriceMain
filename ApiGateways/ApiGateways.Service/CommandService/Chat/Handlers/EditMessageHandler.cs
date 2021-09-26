using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Chat;
using Common.Models.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class EditMessageHandler : HandlerBase<EditMessageCommand, ChatMessages>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public EditMessageHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<ChatMessages> Execute(EditMessageCommand request, CancellationToken cancellationToken)
        {
	        return await _chatServiceCommand.EditMessage(request.MessageId, request.Text, request.UserId);
        }
    }
}
