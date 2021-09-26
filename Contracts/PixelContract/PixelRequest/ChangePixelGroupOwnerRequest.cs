using Common.Models.Pixels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PixelContract.PixelRequest
{
    public class ChangePixelGroupOwnerRequest
    {
        public List<PixelGroup> Groups;
        public Guid UserId { get; set; }
    }
}
