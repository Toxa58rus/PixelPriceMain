using System.Threading.Tasks;
using Contracts.MailContract.MailRespounse;

namespace ApiGateways.Domain.Services.Mail
{
    public interface IMailService
    {
        Task<SendMailRespounse> SendMessage(string UserId);
    }
}
