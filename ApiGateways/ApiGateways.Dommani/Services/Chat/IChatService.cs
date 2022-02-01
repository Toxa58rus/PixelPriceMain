using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateways.Domain.Models.Chat;

namespace ApiGateways.Domain.Services.Chat
{
    public interface IChatService
    {
        Task<ChatRooms> CreateChatCommand(string createUserId, string joinUserId);
        Task<ChatRooms> GetChat(string roomId);
        Task<List<ChatMessages>> GetMessages(string chatId);
        Task<ChatMessages> SendMessage(string chatId, string userId, string message);
        Task<ChatMessages> EditMessage(string messageId, string text, string userId);
        Task<bool> DeleteMessage(string messageId, string userId);
        Task<List<ChatRooms>> GetChatRooms(string userId);
    }
}
