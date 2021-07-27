using Common.Models.Mail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Command.Mail
{
    public class SendMailCommand: IRequest<IActionResult>
    {
        public string UserId { get; set; }
    }
}
