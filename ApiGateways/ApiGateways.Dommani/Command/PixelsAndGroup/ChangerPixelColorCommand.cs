using System;
using System.Collections.Generic;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class ChangerPixelColorCommand : IRequest<List<ChangePixelColorResponseModel>>
    {
        public List<Guid> PixelsId { get; set; }
        public int Color { get; set; }
    }
}
