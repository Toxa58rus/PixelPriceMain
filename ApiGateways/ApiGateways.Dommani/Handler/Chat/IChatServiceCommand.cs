using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models.Chat;

namespace ApiGateways.Dommain.Handler.Chat
{
    public interface IChatServiceCommand
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
