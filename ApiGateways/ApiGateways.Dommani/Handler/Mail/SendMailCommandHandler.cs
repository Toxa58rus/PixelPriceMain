using ApiGateways.Dommain.Command.Mail;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Service.CommandService.Mail;

namespace ApiGateways.Dommain.Handler.Mail
{
    class SendMailCommandHandler : IRequestHandler<SendMailCommand, string>
    {
        private readonly IMailServiceCommand _mailServiceCommand;

        public SendMailCommandHandler(IMailServiceCommand mailServiceCommand)
        {
            this._mailServiceCommand = mailServiceCommand;
        }
        public async Task<string> Handle(SendMailCommand request, CancellationToken cancellationToken)
        {
           return await _mailServiceCommand.SendMessage(request.UserId);
        }
    }
}
