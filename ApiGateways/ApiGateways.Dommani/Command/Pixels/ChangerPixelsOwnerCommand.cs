using MediatR;
using System.Collections.Generic;
using PixelData = Common.Models.Pixels.Pixels;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class ChangerPixelsOwnerCommand : IRequest<List<PixelData>>
    {
        public List<PixelData> Pixels { get; set; }
        public string UserId { get; set; }
    }
}
