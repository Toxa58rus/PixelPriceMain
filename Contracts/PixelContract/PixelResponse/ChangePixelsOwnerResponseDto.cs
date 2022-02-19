using System;
using System.Collections.Generic;

namespace Contracts.PixelContract.PixelResponse
{
    public class ChangePixelsOwnerResponseDto
    {
        public List<Guid> PixelIds { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId{ get; set; }
    }
}
