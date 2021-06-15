using Common.Models.Pixels;
using MediatR;
using System.Collections.Generic;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class ChangerPixelGroupOwnerCommand : IRequest<List<PixelGroup>>
    {
        public List<PixelGroup> Groups { get; set; }
        public string UserId { get; set; }
    }
}
