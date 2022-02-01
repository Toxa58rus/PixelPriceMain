using System.Threading.Tasks;
using ApiGateways.Domain.Models.ImageParser;

namespace ApiGateways.Domain.Services.ImageParser
{
    public interface IImageParserService
    {
        Task<ImageData> ParseImage(ImageData data);
    }
}
