using System;
using System.Collections.Generic;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class ChangerPixelColorCommand : IRequest<IResultWithError>
    {
        public List<PixelData> Pixels { get; set; }
    }
}
