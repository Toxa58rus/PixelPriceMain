using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Service.CommandService.Pixel;
using Common.Models.Pixels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Pixels
{
    public class ChangerPixelGroupOwnerCommandHandler : IRequestHandler<ChangerPixelGroupOwnerCommand, List<PixelGroup>>
    {
        private readonly IPixelServiceCommand _pixelServiceCommand;

        public ChangerPixelGroupOwnerCommandHandler(IPixelServiceCommand pixelServiceCommand)
        {
            _pixelServiceCommand = pixelServiceCommand;
        }

        public async Task<List<PixelGroup>> Handle(ChangerPixelGroupOwnerCommand request, CancellationToken cancellationToken) =>
            await _pixelServiceCommand.ChangerPixelGroupOwner(request.Groups, request.UserId);
    }
}
