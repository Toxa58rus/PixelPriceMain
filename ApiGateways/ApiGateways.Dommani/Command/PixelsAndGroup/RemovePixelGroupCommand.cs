using System;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class RemovePixelGroupCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
    }
}
