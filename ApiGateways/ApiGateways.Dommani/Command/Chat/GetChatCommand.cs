using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Command.Chat
{
    public class GetChatCommand : IRequest<string>
    {
        public string CreateUserId { get; set; }
        public string JoinUserId { get; set; } 
    }
}
