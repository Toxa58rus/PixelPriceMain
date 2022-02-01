using ApiGateways.Domain.Models.Chat;
using MediatR;

namespace ApiGateways.Domain.Command.Chat
{
    public class EditMessageCommand : IRequest<ChatMessages>
    {
        public string MessageId { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
    }
}
