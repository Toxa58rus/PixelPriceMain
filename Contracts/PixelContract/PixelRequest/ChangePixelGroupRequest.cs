using Common.Models.Pixels;
using System;
using System.Collections.Generic;

namespace Contracts.PixelContract.PixelRequest
{
    public class ChangePixelGroupRequest
    {
        public List<Pixel> Pixels;
        public Guid GroupId { get; set; }
    }
}
