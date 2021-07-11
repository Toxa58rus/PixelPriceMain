using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Command.Mail
{
    public class SendMailCommand: IRequest<string>
    {
        public string UserId { get; set; }
    }
}
