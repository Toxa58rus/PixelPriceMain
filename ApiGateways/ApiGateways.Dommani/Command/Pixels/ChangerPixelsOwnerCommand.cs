using MediatR;
using System;
using System.Collections.Generic;
using PixelData = Common.Models.Pixels.Pixel;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class ChangerPixelsOwnerCommand : IRequest<List<PixelData>>
    {
        public List<PixelData> Pixels { get; set; }
        public Guid UserId { get; set; }
    }
}
