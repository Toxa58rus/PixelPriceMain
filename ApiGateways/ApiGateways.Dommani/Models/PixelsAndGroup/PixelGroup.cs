using System;
using MassTransit;

namespace ApiGateways.Domain.Models.PixelsAndGroup
{
    public class PixelGroup
    {
	    public PixelGroup(string name, Guid userId, bool isDefault = false)
        {
            Id = NewId.NextGuid();
            Name = name;
            UserId = userId;
            IsDefault = isDefault;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid UserId { get; private set; }
        public bool IsDefault { get; private set; }
    }
}
