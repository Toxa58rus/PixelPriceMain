using System.Collections.Generic;
using Common.Models.Pixels;
using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.Pixel
{
    public interface IPixelServiceCommand
    {
        Task<PixelGroup> CreateUserPixelGroup(string userId, string name, bool isDefault = false);
        Task<bool> RemovePixelGroup(string id);
        Task<List<Pixels>> ChangerPixelGroup(List<Pixels> pixels, string groupId);
    }
}
