using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Pixels;
using PixelData = Common.Models.Pixels.Pixel;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class ChangerPixelsOwnerHandler : HandlerBase<ChangerPixelsOwnerCommand, List<PixelData>>
    {
        private readonly IPixelServiceCommand _pixelServiceCommand;

        public ChangerPixelsOwnerHandler(IPixelServiceCommand pixelServiceCommand)
        {
            _pixelServiceCommand = pixelServiceCommand;
        }

        protected override async Task<List<PixelData>> Execute(ChangerPixelsOwnerCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelServiceCommand.ChangerPixelsOwner(request.Pixels, request.UserId);
        }
    }
}
