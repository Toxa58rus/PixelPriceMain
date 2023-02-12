using System;
using System.Collections.Generic;

#nullable disable

namespace PixelService.Context.Models
{
    public class Pixel
    {
        public Guid Id { get; set; }
        public int Color { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public Guid PixelGroupId { get; set; }
        public Guid UserId { get; set; }
        public virtual PixelGroup  PixelGroup { get; set; }
    }
}
