using MediatR;
using System.Collections.Generic;
using PixelData = Common.Models.Pixels.Pixels;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class ChangerPixelGroupCommand : IRequest<List<PixelData>>
    {
        public List<PixelData> Pixels { get; set; }
        public string GroupId { get; set; }
    }
}
