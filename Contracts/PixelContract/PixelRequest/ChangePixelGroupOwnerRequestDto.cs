﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PixelContract.PixelRequest
{
    public class ChangePixelGroupOwnerRequestDto
    {
        public List<Guid> GroupIds;
        public Guid UserId { get; set; }
    }
}
