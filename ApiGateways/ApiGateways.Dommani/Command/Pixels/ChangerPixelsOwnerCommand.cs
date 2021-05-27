using MediatR;
using System.Collections.Generic;
using PixelData = Common.Models.Pixels.Pixels;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class ChangerPixelsOwnerCommand : IRequest<string>
    {
        public List<PixelData> Pixels { get; set; }
        public string UserId { get; set; }
    }
}
