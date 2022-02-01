using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;

namespace ApiGateways.Domain.Services.Pixels
{
    public interface IPixelAndGroupService
    {
        Task<PixelGroup> CreateUserPixelGroup(Guid userId, string name, bool isDefault = false);
        Task<bool> RemovePixelGroup(Guid id, Guid groupId);
        Task<List<Pixel>> ChangerPixelGroup(List<Pixel> pixels, Guid groupId);
        Task<List<Pixel>> ChangerPixelsOwner(List<Pixel> pixels, Guid userId);
        Task<List<PixelGroup>> ChangerPixelGroupOwner(List<PixelGroup> groups, Guid userId);
        Task<List<ChangePixelColorResponseModel>> ChangerPixelColor(List<Guid> pixels, int color);
    }
}
