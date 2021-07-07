using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Pixels
{
    public class ChangePixelGroupOwnerResponseModel
    {
        public List<PixelGroup> Groups;
        public string UserId { get; set; }
    }
}
