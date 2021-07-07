using Common.Models.ImageParser;
using MediatR;

namespace ApiGateways.Dommain.Command.ImageParser
{
    public class ParseImageCommand : IRequest<ImageData>
    {
        public string ImageBaseString { get; set; }
        public int XCount { get; set; }
        public int YCount { get; set; }
    }
}
