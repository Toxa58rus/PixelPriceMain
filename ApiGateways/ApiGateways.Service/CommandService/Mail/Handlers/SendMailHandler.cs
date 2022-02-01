using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.Mail;
using ApiGateways.Domain.Services.Mail;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateways.Service.CommandService.Mail.Handlers
{
    public class SendMailHandler : HandlerBase<SendMailCommand, IActionResult>
    {
        private readonly IMailService _mailServiceCommand;

        public SendMailHandler(IMailService mailServiceCommand)
        {
            this._mailServiceCommand = mailServiceCommand;
        }

        protected override async Task<IActionResult> Execute(SendMailCommand request, CancellationToken cancellationToken)
        {
			var temp = await _mailServiceCommand.SendMessage(request.UserId);

			return new OkObjectResult(temp);
		}
    }
}
