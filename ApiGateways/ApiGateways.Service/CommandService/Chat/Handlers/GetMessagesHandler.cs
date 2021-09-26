using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Chat;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Chat;
using Common.Models.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class GetMessagesHandler : HandlerBase<GetMessagesCommand, List<ChatMessages>>
    {
        private readonly IChatServiceCommand _chatServiceCommand;

        public GetMessagesHandler(IChatServiceCommand chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<List<ChatMessages>> Execute(GetMessagesCommand request, CancellationToken cancellationToken)
        {
            return await _chatServiceCommand.GetMessages(request.ChatId);
        }
    }
}
