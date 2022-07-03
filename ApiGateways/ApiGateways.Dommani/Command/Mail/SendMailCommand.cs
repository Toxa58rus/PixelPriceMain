using Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateways.Domain.Command.Mail
{
    public class SendMailCommand: IRequest<IResultWithError>//IRequest<IActionResult>
    {
        public string UserId { get; set; }
    }
}
