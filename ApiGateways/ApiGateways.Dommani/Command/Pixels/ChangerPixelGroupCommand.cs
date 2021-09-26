using MediatR;
using System;
using System.Collections.Generic;
using PixelData = Common.Models.Pixels.Pixel;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class ChangerPixelGroupCommand : IRequest<List<PixelData>>
    {
        public List<PixelData> Pixels { get; set; }
        public Guid GroupId { get; set; }
    }
}
