using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class DeleteMessageHandler : HandlerBase<DeleteMessageCommand, bool>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public DeleteMessageHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<bool> Execute(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
	       return await _chatServiceCommand.DeleteMessage(request.MessageId, request.UserId);
        }

    }
}
