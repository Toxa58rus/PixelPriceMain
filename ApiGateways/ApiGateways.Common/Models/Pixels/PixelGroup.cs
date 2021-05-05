using System;

namespace Common.Models.Pixels
{
    public class PixelGroup
    {
        public PixelGroup()
        {
        }

        public PixelGroup(string name, string userId, bool isDefault = false)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            UserId = userId;
            IsDefault = isDefault;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public bool IsDefault { get; set; }
    }
}
