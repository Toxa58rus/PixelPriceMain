using System.Threading.Tasks;
using Contracts.MailContract.MailRespounse;

namespace ApiGateways.Domain.Services.Mail
{
    public interface IMailService
    {
        Task<SendMailRespounseDto> SendMessage(string UserId);
    }
}
