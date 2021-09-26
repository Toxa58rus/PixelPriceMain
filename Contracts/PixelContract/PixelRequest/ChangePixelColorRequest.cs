using Common.Models.Pixels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PixelContract.PixelRequest
{
    public class ChangePixelColorRequest
    {
        public List<Pixel> Pixels;
        public int Color { get; set; }
    }
}
