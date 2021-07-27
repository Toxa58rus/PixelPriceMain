using ApiGateways.Dommain.Command.Mail;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Service.CommandService.Mail;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateways.Dommain.Handler.Mail
{
   public class SendMailCommandHandler : IRequestHandler<SendMailCommand, IActionResult>
    {
        private readonly IMailServiceCommand _mailServiceCommand;

        public SendMailCommandHandler(IMailServiceCommand mailServiceCommand)
        {
            this._mailServiceCommand = mailServiceCommand;
        }
        public async Task<IActionResult> Handle(SendMailCommand request, CancellationToken cancellationToken)
        {
           var temp =  await _mailServiceCommand.SendMessage(request.UserId);
            
           return new OkObjectResult(temp);
        }
    }
}
