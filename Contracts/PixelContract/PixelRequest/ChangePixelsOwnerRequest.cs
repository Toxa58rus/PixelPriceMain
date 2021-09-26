using Common.Models.Pixels;
using System;
using System.Collections.Generic;

namespace Contracts.PixelContract.PixelRequest
{
    public class ChangePixelsOwnerRequest
    {
        public List<Pixel> Pixels;
        public Guid UserId { get; set; }
    }
}
