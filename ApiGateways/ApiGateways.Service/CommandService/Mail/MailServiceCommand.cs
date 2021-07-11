using Common.Models;
using Common.Models.Mail;
using Common.Rcp;
using Common.Rcp.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.Mail
{
    public class MailServiceCommand : IMailServiceCommand
    {
        private readonly RpcClient _rpcClient;

        public MailServiceCommand(IConfiguration configuration)
        {
            var query = configuration["RpcServer:Querys:Mail"];
            _rpcClient = new RpcClient(new RpcOptions(query));
        }
        public async Task<string> SendMessage(string UserId )
        {
            var command = new CommandResponse
            {
                CommandName = "SendMail",
                Value = new SendMailModel
                {
                    UserId = UserId
                }
            };

            return await _rpcClient.SendCommandToServer<string>(command);
        }
    }
}
