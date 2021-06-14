using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.ImageParser
{
    public interface IImageParserServiceCommand
    {
        Task<string> ParceImagetoBitmap(string baseString);
    }
}
