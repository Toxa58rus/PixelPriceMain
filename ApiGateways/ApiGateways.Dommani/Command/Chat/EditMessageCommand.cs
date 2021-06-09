using Common.Models.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Command.Chat
{
    public class EditMessageCommand : IRequest<ChatMessages>
    {
        public string MessageId { get; set; }
        public string NewText { get; set; }
    }
}
