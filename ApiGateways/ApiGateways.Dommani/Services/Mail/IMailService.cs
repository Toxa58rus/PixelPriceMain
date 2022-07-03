using System.Threading.Tasks;
using Common.Errors;
using Contracts.MailContract.MailRespounse;

namespace ApiGateways.Domain.Services.Mail
{
    public interface IMailService
    {
        Task<IResultWithError<SendMailRespounseDto>> /*SendMailRespounseDto> */SendMessage(string UserId);
    }
}
