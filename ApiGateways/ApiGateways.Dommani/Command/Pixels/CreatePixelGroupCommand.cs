using Common.Models.Pixels;
using MediatR;
using System;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class CreatePixelGroupCommand : IRequest<PixelGroup>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
