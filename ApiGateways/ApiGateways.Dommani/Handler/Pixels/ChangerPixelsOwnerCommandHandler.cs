using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Service.CommandService.Pixel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Pixels
{
    public class ChangerPixelsOwnerCommandHandler : IRequestHandler<ChangerPixelsOwnerCommand, string>
    {
        private readonly IPixelServiceCommand _pixelServiceCommand;

        public ChangerPixelsOwnerCommandHandler(IPixelServiceCommand pixelServiceCommand)
        {
            _pixelServiceCommand = pixelServiceCommand;
        }

        public async Task<string> Handle(ChangerPixelsOwnerCommand request, CancellationToken cancellationToken) =>
            await _pixelServiceCommand.ChangerPixelsOwner(request.Pixels, request.UserId);
    }
}
