using ApiGateways.Dommain.Command.Mail;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateways.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MailController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> SendMail([FromBody] SendMailCommand command) =>
           await Mediator.Send(command);
    }
}
