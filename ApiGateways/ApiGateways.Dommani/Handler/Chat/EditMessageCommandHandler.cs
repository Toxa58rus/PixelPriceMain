using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Service.CommandService.Chat;
using Common.Models.Chat;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Chat
{
    public class EditMessageCommandHandler : IRequestHandler<EditMessageCommand, ChatMessages>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public EditMessageCommandHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        public async Task<ChatMessages> Handle(EditMessageCommand request, CancellationToken cancellationToken) =>
            await _chatServiceCommand.EditMessage(request.MessageId, request.Text, request.UserId);
    }
}
