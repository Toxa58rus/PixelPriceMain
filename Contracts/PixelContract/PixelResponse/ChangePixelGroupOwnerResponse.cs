using System;
using System.Collections.Generic;

namespace Contracts.PixelContract.PixelResponse
{
    public class ChangePixelGroupOwnerResponse
    {
        public List<Guid> GroupIds;
        public Guid UserId { get; set; }
    }
}
