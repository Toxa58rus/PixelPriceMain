using ApiGateways.Domain.Models.Chat;
using MediatR;

namespace ApiGateways.Domain.Command.Chat
{
    public class SendMessageCommand : IRequest<ChatMessages>
    {
        public string ChatId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
