using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Service.CommandService.Pixel;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PixelData = Common.Models.Pixels.Pixels;

namespace ApiGateways.Dommain.Handler.Pixels
{
    public class ChangerPixelGroupCommandHandler : IRequestHandler<ChangerPixelGroupCommand, List<PixelData>>
    {
        private readonly IPixelServiceCommand _pixelServiceCommand;

        public ChangerPixelGroupCommandHandler(IPixelServiceCommand pixelServiceCommand)
        {
            _pixelServiceCommand = pixelServiceCommand;
        }

        public async Task<List<PixelData>> Handle(ChangerPixelGroupCommand request, CancellationToken cancellationToken) =>
            await _pixelServiceCommand.ChangerPixelGroup(request.Pixels, request.GroupId);
    }
}
