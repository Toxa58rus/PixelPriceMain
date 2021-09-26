using MassTransit;
using System;

namespace Common.Models.Pixels
{
    public class PixelColor
    {
	    public PixelColor(Guid pixelId, int color)
        {
            Id = NewId.NextGuid();
            PixelId = pixelId;
            Color = color;
        }

        public Guid Id { get; set; }
        public Guid PixelId { get; set; }
        public int Color { get; set; }
    }
}
