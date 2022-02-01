using System;
using System.Collections.Generic;

namespace ApiGateways.Domain.Models.PixelsAndGroup.Response
{
     public class ChangePixelColorResponseModel
    {
        public List<Guid> PixelId { get; set; }
        public int Color { get; set; }
    }
}
