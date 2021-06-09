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
    public class GetChatCommandHandler : IRequestHandler<GetChatCommand, string>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public GetChatCommandHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        public async Task<string> Handle(GetChatCommand request, CancellationToken cancellationToken) =>
             await _chatServiceCommand.GetChat(request.CreateUserId, request.JoinUserId);
    }
}

