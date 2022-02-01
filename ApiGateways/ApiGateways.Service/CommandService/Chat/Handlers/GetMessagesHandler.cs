using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.Chat;
using ApiGateways.Domain.Models.Chat;
using ApiGateways.Domain.Services.Chat;

namespace ApiGateways.Service.CommandService.Chat.Handlers
{
    public class GetMessagesHandler : HandlerBase<GetMessagesCommand, List<ChatMessages>>
    {
        private readonly IChatService _chatServiceCommand;

        public GetMessagesHandler(IChatService chatServiceCommand)
        {
            _chatServiceCommand = chatServiceCommand;
        }

        protected override async Task<List<ChatMessages>> Execute(GetMessagesCommand request, CancellationToken cancellationToken)
        {
            return await _chatServiceCommand.GetMessages(request.ChatId);
        }
    }
}
