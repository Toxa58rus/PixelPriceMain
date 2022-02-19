using System;
using System.Collections.Generic;

namespace ApiGateways.Domain.Models.PixelsAndGroup.Response
{
    public class ChangePixelsResponse
    {
        public List<Pixel> Pixels;
        public Guid GroupId { get; set; }
    }
}
