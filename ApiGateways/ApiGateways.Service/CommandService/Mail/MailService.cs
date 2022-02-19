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
        private readonly IRequestClient<SendMailRequestDto> _requestClient;

        public MailService(IConfiguration configuration, IRequestClient<SendMailRequestDto> requestClient)
        {
            _requestClient = requestClient;
        }
        public async Task<SendMailRespounseDto> SendMessage(string userId)
        {
            var value = new SendMailRequestDto()
            {
                UserId = Guid.Parse(userId)
            };

            var result = await _requestClient.GetResponse<SendMailRespounseDto>(value);

            return result.Message;
        }
    }
}
