using System;
using System.Collections.Generic;
using ApiGateways.Domain.Models.PixelsAndGroup;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class ChangerPixelGroupOwnerCommand : IRequest<List<PixelGroup>>
    {
        public List<PixelGroup> Groups { get; set; }
        public Guid UserId { get; set; }
    }
}
