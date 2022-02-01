using ApiGateways.Domain.Models.Chat;
using MediatR;

namespace ApiGateways.Domain.Command.Chat
{
    public class GetChatCommand : IRequest<ChatRooms>
    {
        public string RoomId { get; set; }
    }
}
