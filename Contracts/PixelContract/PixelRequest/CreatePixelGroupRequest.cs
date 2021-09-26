using Common.Models.Pixels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PixelContract.PixelRequest
{
    public class CreatePixelGroupRequest
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

    }
}
