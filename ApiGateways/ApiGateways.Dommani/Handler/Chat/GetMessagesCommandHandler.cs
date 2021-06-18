using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Service.CommandService.Chat;
using Common.Models.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Chat
{
    public class GetMessagesCommandHandler : IRequestHandler<GetMessagesCommand, List<ChatMessages>>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public GetMessagesCommandHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        public async Task<List<ChatMessages>> Handle(GetMessagesCommand request, CancellationToken cancellationToken) =>
             await _chatServiceCommand.GetMessages(request.ChatId);
    }
}
