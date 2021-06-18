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
    public class GetUserChatsCommandHandler : IRequestHandler<GetUserChatsCommand, List<ChatRooms>>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public GetUserChatsCommandHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        public async Task<List<ChatRooms>> Handle(GetUserChatsCommand request, CancellationToken cancellationToken) =>
             await _chatServiceCommand.GetChatRooms(request.UserId);
    }
}
