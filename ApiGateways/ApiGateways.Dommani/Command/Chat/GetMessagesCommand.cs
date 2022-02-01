using System.Collections.Generic;
using ApiGateways.Domain.Models.Chat;
using MediatR;

namespace ApiGateways.Domain.Command.Chat
{
    public class GetMessagesCommand : IRequest<List<ChatMessages>>
    {
        public string ChatId { get; set; }
    }
}
