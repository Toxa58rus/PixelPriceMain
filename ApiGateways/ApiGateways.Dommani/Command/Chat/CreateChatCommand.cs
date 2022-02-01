using ApiGateways.Domain.Models.Chat;
using MediatR;

namespace ApiGateways.Domain.Command.Chat
{
    public class CreateChatCommand : IRequest<ChatRooms>
    {
        public string CreateUserId { get; set; }
        public string JoinUserId { get; set; }
    }
}
