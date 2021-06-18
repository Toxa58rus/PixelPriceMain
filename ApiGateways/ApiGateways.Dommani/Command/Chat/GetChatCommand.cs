using Common.Models.Chat;
using MediatR;

namespace ApiGateways.Dommain.Command.Chat
{
    public class GetChatCommand : IRequest<ChatRooms>
    {
        public string RoomId { get; set; }
    }
}
