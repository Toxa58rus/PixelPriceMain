using System;
using System.Threading.Tasks;
using ApiGateways.Domain.Services.Mail;
using Contracts.MailContract.MailRequest;
using Contracts.MailContract.MailRespounse;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace ApiGateways.Service.CommandService.Mail
{
    public class MailService : IMailService
    {
        private readonly IRequestClient<SendMailRequest> _requestClient;

        public MailService(IConfiguration configuration, IRequestClient<SendMailRequest> requestClient)
        {
            _requestClient = requestClient;
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
