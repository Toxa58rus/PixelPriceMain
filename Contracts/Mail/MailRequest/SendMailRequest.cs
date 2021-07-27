using System;

namespace Contracts.Mail.MailRequest
{
    public interface ISendMailRequest
    {
        public Guid UserId { get; set; }
    }
    public class SendMailRequest 
    {
        public Guid UserId { get; set; }
    }
}
