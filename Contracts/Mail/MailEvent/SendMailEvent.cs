using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Mail.MailEvent
{
    public class SendMailEvent 
    {
        public Guid UserId { get; set; }
    }
}
