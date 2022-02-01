using System;
using ApiGateways.Domain.Models.PixelsAndGroup;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class CreatePixelGroupCommand : IRequest<PixelGroup>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
