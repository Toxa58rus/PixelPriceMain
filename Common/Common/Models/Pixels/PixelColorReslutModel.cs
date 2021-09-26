using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Pixels
{
     public class PixelColorReslutModel
    {
        public Guid PixelId { get; set; }
        public List<int> Color { get; set; }
    }
}
