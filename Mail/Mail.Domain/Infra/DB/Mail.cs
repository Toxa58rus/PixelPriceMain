using System;

namespace MailService.Domain.Infra.DB
{
    public class Mail
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string MailAddress { get; set; }
    }
}
