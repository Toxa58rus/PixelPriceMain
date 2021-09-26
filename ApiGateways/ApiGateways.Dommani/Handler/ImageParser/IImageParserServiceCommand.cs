using System.Threading.Tasks;
using Common.Models.ImageParser;

namespace ApiGateways.Dommain.Handler.ImageParser
{
    public interface IImageParserServiceCommand
    {
        Task<ImageData> ParseImage(ImageData data);
    }
}
