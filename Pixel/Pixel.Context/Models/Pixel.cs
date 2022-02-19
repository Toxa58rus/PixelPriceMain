using System;
using System.Collections.Generic;

#nullable disable

namespace PixelService.Context.Models
{
    public partial class Pixel
    {
        public Guid Id { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
    }
}
