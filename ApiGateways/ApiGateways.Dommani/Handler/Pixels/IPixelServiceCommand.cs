using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models.Pixels;

namespace ApiGateways.Dommain.Handler.Pixels
{
    public interface IPixelServiceCommand
    {
        Task<PixelGroup> CreateUserPixelGroup(Guid userId, string name, bool isDefault = false);
        Task<bool> RemovePixelGroup(Guid id, Guid groupId);
        Task<List<Pixel>> ChangerPixelGroup(List<Pixel> pixels, Guid groupId);
        Task<List<Pixel>> ChangerPixelsOwner(List<Pixel> pixels, Guid userId);
        Task<List<PixelGroup>> ChangerPixelGroupOwner(List<PixelGroup> groups, Guid userId);
        Task<List<PixelColorReslutModel>> ChangerPixelColor(List<Pixel> pixels, int color);
    }
}
