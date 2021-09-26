using System;
using System.Collections.Generic;

#nullable disable

namespace PixelService.Context.Models
{
    public partial class PixelGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public bool IsDefault { get; set; }
    }
}
