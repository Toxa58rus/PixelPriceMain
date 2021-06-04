using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.Chat
{
    public interface IChatServiceCommand
    {
        Task<string> CreateChatCommand();
    }
}
