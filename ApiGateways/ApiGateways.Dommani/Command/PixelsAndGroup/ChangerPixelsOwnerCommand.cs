using System;
using System.Collections.Generic;
using MediatR;
using PixelData = ApiGateways.Domain.Models.PixelsAndGroup.Pixel;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class ChangerPixelsOwnerCommand : IRequest<List<PixelData>>
    {
        public List<PixelData> Pixels { get; set; }
        public Guid UserId { get; set; }
    }
}
