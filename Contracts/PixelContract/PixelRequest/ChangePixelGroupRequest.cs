using System;
using System.Collections.Generic;

namespace Contracts.PixelContract.PixelRequest
{
    public class ChangePixelGroupRequest
    {
        public List<Guid> PixelIds;
        public Guid GroupId { get; set; }
    }
}
