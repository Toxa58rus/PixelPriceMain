using System.Threading.Tasks;
using Contracts.MailContract.MailRespounse;

namespace ApiGateways.Dommain.Handler.Mail
{
    public interface IMailServiceCommand
    {
        Task<SendMailRespounse> SendMessage(string UserId);
    }
}
