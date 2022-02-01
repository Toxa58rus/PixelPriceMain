using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.Chat;
using ApiGateways.Domain.Models.Chat;
using ApiGateways.Domain.Services.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class EditMessageHandler : HandlerBase<EditMessageCommand, ChatMessages>
    {
        private readonly IChatService _chatServiceCommand;

        public EditMessageHandler(IChatService chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<ChatMessages> Execute(EditMessageCommand request, CancellationToken cancellationToken)
        {
	        return await _chatServiceCommand.EditMessage(request.MessageId, request.Text, request.UserId);
        }
    }
}
