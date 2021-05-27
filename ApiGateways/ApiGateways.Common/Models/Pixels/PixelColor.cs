using System;

namespace Common.Models.Pixels
{
    public class PixelColor
    {
        public PixelColor()
        {
        }

        public PixelColor(string pixelId, string color)
        {
            Id = Guid.NewGuid().ToString();
            PixelId = pixelId;
            Color = color;
        }

        public string Id { get; set; }
        public string PixelId { get; set; }
        public string Color { get; set; }
    }
}
