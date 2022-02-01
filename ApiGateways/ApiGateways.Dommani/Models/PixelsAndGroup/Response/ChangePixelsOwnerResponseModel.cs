using System;
using System.Collections.Generic;

namespace ApiGateways.Domain.Models.PixelsAndGroup.Response
{
    public class ChangePixelsOwnerResponseModel
    {
        public List<Pixel> Pixels;
        public Guid UserId { get; set; }
    }
}
