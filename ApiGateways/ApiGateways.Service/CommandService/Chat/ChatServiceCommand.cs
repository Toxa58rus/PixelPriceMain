using Common.Extensions;
using Common.Models;
using Common.Models.Chat;
using Common.Rcp;
using Common.Rcp.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.Chat
{
    public class ChatServiceCommand : IChatServiceCommand
    {
        private readonly RpcClient _rpcClient;

        public ChatServiceCommand(IConfiguration configuration)
        {
            var query = configuration["RpcServer:Querys:Chat"];
            _rpcClient = new RpcClient(new RpcOptions(query));
        }

        public async Task<string> CreateChatCommand()
        {
            var command = new CommandResponce
            {
                CommandName = "CreateChat",
                Value = "test string"
            };
            return await SendCommandToServer<string>(command);
        }

        public async Task<string> GetChat(string createUserId, string joinUserId)
        {
            var command = new CommandResponce
            {
                CommandName = "GetChat",
                Value = new ChatRooms()
                {
                    CreateUserId = createUserId,
                    JoinUserId = joinUserId
                }
            };

            return await SendCommandToServer<string>(command);
        }

        public async Task<List<ChatMessages>> GetMessages(string chatId)
        {
            var command = new CommandResponce
            {
                CommandName = "GetMessages",
                Value = new GetChatMessagesResponseModel()
                {
                    ChatId = chatId
                }
            };
            return await SendCommandToServer<List<ChatMessages>>(command);
        }

        public async Task<ChatMessages> SendMessage(string chatId, string userId, string message)
        {
            var command = new CommandResponce
            {
                CommandName = "SendMessage",
                Value = new ChatMessages(chatId, userId, message)

            };
            return await SendCommandToServer<ChatMessages>(command);
        }

        public async Task<ChatMessages> EditMessage(string messageId, string text, string userId)
        {
            var command = new CommandResponce
            {
                CommandName = "EditMessage",
                Value = new EditMessageResponseModel()
                {
                    MessageId = messageId,
                    Text = text,
                    UserId = userId
                }
            };
            return await SendCommandToServer<ChatMessages>(command);
        }

        public async Task<bool> DeleteMessage(string messageId, string userId)
        {
            var command = new CommandResponce
            {
                CommandName = "DeleteMessage",
                Value = new DeleteMessageResponseModel()
                {
                    MessageId = messageId,
                    UserId = userId
                }
            };
            return await SendCommandToServer<bool>(command);
        }


        private async Task<T> SendCommandToServer<T>(CommandResponce command)
        {
            var responce = await _rpcClient.CallAsync(command.ToJson(), CancellationToken.None);
            _rpcClient.Dispose();

            return responce.DeserializeToObject<T>();
        }
    }
}
