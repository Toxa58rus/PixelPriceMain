using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.MailContract.MailEvent
{
    public class SendMailEvent
    {
        public Guid UserId { get; set; }
    }
}
