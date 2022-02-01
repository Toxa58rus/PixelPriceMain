using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.Chat;
using ApiGateways.Domain.Services.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class DeleteMessageHandler : HandlerBase<DeleteMessageCommand, bool>
    {
        private readonly IChatService _chatServiceCommand;

        public DeleteMessageHandler(IChatService chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<bool> Execute(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
	       return await _chatServiceCommand.DeleteMessage(request.MessageId, request.UserId);
        }

    }
}
