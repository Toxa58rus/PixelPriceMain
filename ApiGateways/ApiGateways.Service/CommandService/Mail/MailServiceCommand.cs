using Common.Models;
using Common.Models.Mail;
using Common.Rcp;
using Common.Rcp.Client;
using Contracts.Mail.MailRequest;
using Contracts.Mail.MailRespounse;
using MassTransit;
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
        private readonly IRequestClient<SendMailRequest> _requestClient;

        public MailServiceCommand(IConfiguration configuration, IRequestClient<SendMailRequest> requestClien)
        {
            this._requestClient = requestClien;
        }
        public async Task<SendMailRespounse> SendMessage(string UserId )
        {
            var Value = new SendMailRequest()
            {
                UserId = Guid.Parse(UserId)
            };
            
            
            var result = await _requestClient.GetResponse<SendMailRespounse>(Value);
            
            return result.Message;
        }
    }
}
