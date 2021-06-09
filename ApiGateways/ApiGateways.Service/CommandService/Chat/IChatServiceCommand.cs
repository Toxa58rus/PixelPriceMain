using Common.Models.Chat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.Chat
{
    public interface IChatServiceCommand
    {
        Task<string> CreateChatCommand();
        Task<string> GetChat(string createUserId, string joinUserId);
        Task<List<ChatMessages>> GetMessages(string chatId);
        Task<ChatMessages> SendMessage(string chatId, string userId, string message);
        Task<ChatMessages> EditMessage(string messageId, string newText);
        Task<bool> DeleteMessage(string messageId);
    }
}
