using Common.Models.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Command.Chat
{
    public class SendMessageCommand : IRequest<ChatMessages>
    {
        public string ChatId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
