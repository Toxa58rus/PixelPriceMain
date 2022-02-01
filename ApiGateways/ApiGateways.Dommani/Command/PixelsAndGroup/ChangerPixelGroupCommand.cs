using System;
using System.Collections.Generic;
using MediatR;
using PixelData = ApiGateways.Domain.Models.PixelsAndGroup.Pixel;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class ChangerPixelGroupCommand : IRequest<List<PixelData>>
    {
        public List<PixelData> Pixels { get; set; }
        public Guid GroupId { get; set; }
    }
}
