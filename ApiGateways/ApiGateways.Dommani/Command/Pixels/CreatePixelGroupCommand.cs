using Common.Models.Pixels;
using MediatR;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class CreatePixelGroupCommand : IRequest<PixelGroup>
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
