using System;
using System.Collections.Generic;
using System.Text;

namespace Mail.Domain.Model.DB
{
    public class Mail
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string MailAddress { get; set; }
    }
}
