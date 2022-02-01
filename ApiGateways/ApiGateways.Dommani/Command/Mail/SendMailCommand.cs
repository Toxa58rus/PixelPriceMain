using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateways.Domain.Command.Mail
{
    public class SendMailCommand: IRequest<IActionResult>
    {
        public string UserId { get; set; }
    }
}
