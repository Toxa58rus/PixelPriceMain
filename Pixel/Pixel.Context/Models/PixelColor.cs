using System;
using System.Collections.Generic;

#nullable disable

namespace PixelService.Context.Models
{
    public partial class PixelColor
    {
        public Guid Id { get; set; }
        public Guid PixelId { get; set; }
        public int Color { get; set; }
    }
}
