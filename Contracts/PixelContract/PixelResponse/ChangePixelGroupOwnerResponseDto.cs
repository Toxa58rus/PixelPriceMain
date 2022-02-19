using System;
using System.Collections.Generic;

namespace Contracts.PixelContract.PixelResponse
{
    public class ChangePixelGroupOwnerResponseDto
    {
        public List<Guid> GroupIds;
        public Guid UserId { get; set; }
    }
}
