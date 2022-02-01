using System.Collections.Generic;
using ApiGateways.Domain.Models.Chat;
using MediatR;

namespace ApiGateways.Domain.Command.Chat
{
    public class GetUserChatsCommand : IRequest<List<ChatRooms>>
    {
       public string UserId { get; set; }
    }
}
