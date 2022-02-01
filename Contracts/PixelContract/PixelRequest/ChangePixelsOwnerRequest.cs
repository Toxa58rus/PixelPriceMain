using System;
using System.Collections.Generic;

namespace Contracts.PixelContract.PixelRequest
{
    public class ChangePixelsOwnerRequest
    {
        public List<Guid> PixelIds;
        public Guid UserId { get; set; }
    }
}
