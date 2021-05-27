using Common.Models.Pixels;
using MediatR;
using System.Collections.Generic;
using PixelData = Common.Models.Pixels.Pixels;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class ChangerPixelColorCommand : IRequest<List<PixelColor>>
    {
        public List<PixelData> Pixels { get; set; }
        public string Color { get; set; }
    }
}
