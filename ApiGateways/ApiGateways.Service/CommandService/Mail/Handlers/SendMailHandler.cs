using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Mail;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Mail;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateways.Service.CommandService.Mail.Handlers
{
    public class SendMailHandler : HandlerBase<SendMailCommand, IActionResult>
    {
        private readonly IMailServiceCommand _mailServiceCommand;

        public SendMailHandler(IMailServiceCommand mailServiceCommand)
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
