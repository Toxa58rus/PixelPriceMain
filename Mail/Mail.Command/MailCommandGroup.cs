using Common.Rcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Command
{
    public class MailCommandGroup : CommandGroup
    {
        public MailCommandGroup()
        {
            var command = new List<ServiceCommand>
            {
                new SendMailCommand()
            };

            SetDefaultCommands(command);
        }
    }
}
