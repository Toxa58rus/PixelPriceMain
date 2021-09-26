using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Pixels;
using PixelData = Common.Models.Pixels.Pixel;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class ChangerPixelGroupHandler : HandlerBase<ChangerPixelGroupCommand, List<PixelData>>
    {
        private readonly IPixelServiceCommand _pixelServiceCommand;

        public ChangerPixelGroupHandler(IPixelServiceCommand pixelServiceCommand)
        {
            _pixelServiceCommand = pixelServiceCommand;
        }

        protected override async Task<List<PixelData>> Execute(ChangerPixelGroupCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelServiceCommand.ChangerPixelGroup(request.Pixels, request.GroupId);
        }
    }
}
