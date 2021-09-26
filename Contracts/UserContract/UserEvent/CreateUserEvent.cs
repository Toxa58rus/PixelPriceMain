using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserContract.UserEvent
{
    public class CreateUserEvent
    {
        public Guid Userid { get; set; }
        public string MailAddress { get; set; }

    }
}
