using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.Mail;
using ApiGateways.Domain.Services.Mail;
using Common.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateways.Service.CommandService.Mail.Handlers
{
    public class SendMailHandler : HandlerBase<SendMailCommand, IResultWithError>//IActionResult>
    {
        private readonly IMailService _mailServiceCommand;

        public SendMailHandler(IMailService mailServiceCommand)
        {
            this._mailServiceCommand = mailServiceCommand;
        }

        protected override async Task<IResultWithError> Execute(SendMailCommand request, CancellationToken cancellationToken)
        {

	        return await _mailServiceCommand.SendMessage(request.UserId);
        }
    }
}
