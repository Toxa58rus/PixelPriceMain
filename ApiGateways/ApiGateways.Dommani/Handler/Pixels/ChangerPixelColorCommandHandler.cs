using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Service.CommandService.Pixel;
using Common.Models.Pixels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Pixels
{
    public class ChangerPixelColorCommandHandler : IRequestHandler<ChangerPixelColorCommand, PixelColor>
    {
        private readonly IPixelServiceCommand _pixelServiceCommand;

        public ChangerPixelColorCommandHandler(IPixelServiceCommand pixelServiceCommand)
        {
            _pixelServiceCommand = pixelServiceCommand;
        }

        public async Task<PixelColor> Handle(ChangerPixelColorCommand request, CancellationToken cancellationToken) =>
            await _pixelServiceCommand.ChangerPixelColor(request.Pixels, request.Color);
    }
}
