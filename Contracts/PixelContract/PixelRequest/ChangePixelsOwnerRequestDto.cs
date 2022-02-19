using System;
using System.Collections.Generic;

namespace Contracts.PixelContract.PixelRequest
{
    public class ChangePixelsOwnerRequestDto
    {
        public List<Guid> PixelIds;
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }
}
