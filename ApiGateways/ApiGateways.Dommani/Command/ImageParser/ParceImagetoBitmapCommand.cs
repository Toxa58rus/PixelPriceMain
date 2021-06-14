using MediatR;

namespace ApiGateways.Dommain.Command.ImageParser
{
    public class ParceImagetoBitmapCommand : IRequest<string>
    {
        public string ImageBaseString { get; set; }
    }
}
