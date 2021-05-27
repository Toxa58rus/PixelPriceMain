using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Service.CommandService.Pixel;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PixelData = Common.Models.Pixels.Pixels;

namespace ApiGateways.Dommain.Handler.Pixels
{
    public class ChangerPixelsOwnerCommandHandler : IRequestHandler<ChangerPixelsOwnerCommand, List<PixelData>>
    {
        private readonly IPixelServiceCommand _pixelServiceCommand;

        public ChangerPixelsOwnerCommandHandler(IPixelServiceCommand pixelServiceCommand)
        {
            _pixelServiceCommand = pixelServiceCommand;
        }

        public async Task<List<PixelData>> Handle(ChangerPixelsOwnerCommand request, CancellationToken cancellationToken) =>
            await _pixelServiceCommand.ChangerPixelsOwner(request.Pixels, request.UserId);
    }
}
