using System;
using System.Collections.Generic;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class GetPixelPartCommand : IRequest<IResultWithError<GetPixelPartResponse>>//IRequest<ResultWithError<GetPixelPartResponse>>
    {
        public int StartPositionX{ get; set; }
        public int StartPositionY { get; set; }
        public int EndPositionX { get; set; }
        public int EndPositionY { get; set; }
    }
}
