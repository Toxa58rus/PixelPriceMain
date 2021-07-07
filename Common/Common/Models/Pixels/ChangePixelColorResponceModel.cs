using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Pixels
{
    public class ChangePixelColorResponseModel
    {
        public List<Pixels> Pixels;
        public string Color { get; set; }
    }
}
