using System;
using System.Threading.Tasks;
using ApiGateways.Dommain.Handler.Mail;
using Contracts.MailContract.MailRequest;
using Contracts.MailContract.MailRespounse;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace ApiGateways.Service.CommandService.Mail
{
    public class MailService : IMailServiceCommand
    {
        private readonly IRequestClient<SendMailRequest> _requestClient;

        public MailService(IConfiguration configuration, IRequestClient<SendMailRequest> requestClien)
        {
            _requestClient = requestClien;
        }
        public async Task<SendMailRespounse> SendMessage(string UserId)
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
