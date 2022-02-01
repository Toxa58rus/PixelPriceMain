using System;
using System.Collections.Generic;

namespace Contracts.PixelContract.PixelResponse
{
    public class ChangePixelsOwnerResponse
    {
        public List<Guid> PixelIds;
        public Guid UserId { get; set; }
    }
}
