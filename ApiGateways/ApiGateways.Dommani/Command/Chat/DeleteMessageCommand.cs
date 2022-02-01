using MediatR;

namespace ApiGateways.Domain.Command.Chat
{
    public class DeleteMessageCommand : IRequest<bool>
    {
        public string MessageId { get; set; }
        public string UserId { get; set; }
    }
}
