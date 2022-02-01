using System;
using System.Collections.Generic;

namespace ApiGateways.Domain.Models.PixelsAndGroup.Response
{
    public class ChangePixelsResponseModel
    {
        public List<Pixel> Pixels;
        public Guid GroupId { get; set; }
    }
}
