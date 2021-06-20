using Common.Models.ImageParser;
using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.ImageParser
{
    public interface IImageParserServiceCommand
    {
        Task<ImageData> ParceImagetoBitmap(ImageData data);
    }
}
