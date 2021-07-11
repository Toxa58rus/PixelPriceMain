using Common.Extensions;
using Common.Models;
using Common.Models.Chat;
using Common.Rcp;
using Common.Rcp.Client;
using Microsoft.Extensions.Configuration;
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

        public async Task<ChatRooms> CreateChatCommand(string createUserId, string joinUserId)
        {
            var command = new CommandResponse
            {
                CommandName = "CreateChat",
                Value = new ChatRooms(createUserId, joinUserId)
            };
            return await _rpcClient.SendCommandToServer<ChatRooms>(command);
        }

        public async Task<ChatRooms> GetChat(string roomId)
        {
            var command = new CommandResponse
            {
                CommandName = "GetChat",
                Value = new GetChatResponseModel()
                {
                   RoomId = roomId
                }
            };
            return await _rpcClient.SendCommandToServer< ChatRooms>(command);
        }

        public async Task<List<ChatMessages>> GetMessages(string chatId)
        {
            var command = new CommandResponse
            {
                CommandName = "GetMessages",
                Value = new GetChatMessagesResponseModel()
                {
                    ChatId = chatId
                }
            };
            return await _rpcClient.SendCommandToServer<List<ChatMessages>>(command);
        }

        public async Task<ChatMessages> SendMessage(string chatId, string userId, string message)
        {
            var command = new CommandResponse
            {
                CommandName = "SendMessage",
                Value = new ChatMessages(chatId, userId, message)

            };
            return await _rpcClient.SendCommandToServer<ChatMessages>(command);
        }

        public async Task<ChatMessages> EditMessage(string messageId, string text, string userId)
        {
            var command = new CommandResponse
            {
                CommandName = "EditMessage",
                Value = new EditMessageResponseModel()
                {
                    MessageId = messageId,
                    Text = text,
                    UserId = userId
                }
            };
            return await _rpcClient.SendCommandToServer<ChatMessages>(command);
        }

        public async Task<bool> DeleteMessage(string messageId, string userId)
        {
            var command = new CommandResponse
            {
                CommandName = "DeleteMessage",
                Value = new DeleteMessageResponseModel()
                {
                    MessageId = messageId,
                    UserId = userId
                }
            };
            return await _rpcClient.SendCommandToServer<bool>(command);
        }

        public async Task<List<ChatRooms>> GetChatRooms(string userId)
        {
            var command = new CommandResponse
            {
                CommandName = "GetRooms",
                Value = new GetRoomsResponseModel()
                {
                    UserId = userId
                }
            };
            return await _rpcClient.SendCommandToServer<List<ChatRooms>>(command);
            
        }
    }
}
