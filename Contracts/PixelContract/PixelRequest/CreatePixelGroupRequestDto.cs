using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PixelContract.PixelRequest
{
    public class CreatePixelGroupRequestDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }

    }
}
