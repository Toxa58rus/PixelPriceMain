using System;
using System.Collections.Generic;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Models.PixelsAndGroup.Response
{
    public class GetPixelByGroupIdResponse
    {
        public List<Pixel> Pixels { get; set; }

    }
}
