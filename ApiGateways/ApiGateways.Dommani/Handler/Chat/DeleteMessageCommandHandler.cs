using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Service.CommandService.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Chat
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public DeleteMessageCommandHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken) =>
             await _chatServiceCommand.DeleteMessage(request.MessageId, request.UserId);
    }
}
