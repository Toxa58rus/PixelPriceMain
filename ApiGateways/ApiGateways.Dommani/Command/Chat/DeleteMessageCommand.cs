using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Command.Chat
{
    public class DeleteMessageCommand : IRequest<bool>
    {
        public string MessageId { get; set; }
        public string UserId { get; set; }
    }
}
