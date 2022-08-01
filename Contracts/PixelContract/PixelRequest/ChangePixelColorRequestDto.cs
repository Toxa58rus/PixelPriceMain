using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PixelContract.PixelRequest
{
    public class ChangePixelColorRequestDto
    {
        public List<PixelDataDto> Pixels { get; set; }
        public int Color { get; set; }
        public Guid UserId { get; set; }
    }
}
