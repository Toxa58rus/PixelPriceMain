﻿using Common.Extensions;
using Common.Models;
using Common.Rcp;
using Common.Rcp.Client;
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

        private async Task<T> SendCommandToServer<T>(CommandResponce command)
        {
            var responce = await _rpcClient.CallAsync(command.ToJson(), CancellationToken.None);
            _rpcClient.Dispose();

            return responce.DeserializeToObject<T>();
        }
    }
}
