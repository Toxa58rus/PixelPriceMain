using MediatR;
using System;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class RemovePixeGrpupCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
    }
}
