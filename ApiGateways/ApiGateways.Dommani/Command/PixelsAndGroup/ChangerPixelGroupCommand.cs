using System;
using System.Collections.Generic;
using Common.Errors;
using MediatR;
using PixelData = ApiGateways.Domain.Models.PixelsAndGroup.Pixel;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class ChangerPixelGroupCommand : IRequest<IResultWithError>
    {
        public List<Guid> Pixels { get; set; }
        public Guid GroupId { get; set; }
    }
}
