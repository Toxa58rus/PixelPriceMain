using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Pixels;
using Common.Models.Pixels;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class ChangerPixelColorHandler : HandlerBase<ChangerPixelColorCommand, List<PixelColorReslutModel>>
    {
        private readonly IPixelServiceCommand _pixelServiceCommand;

        public ChangerPixelColorHandler(IPixelServiceCommand pixelServiceCommand)
        {
            _pixelServiceCommand = pixelServiceCommand;
        }

        protected override async Task<List<PixelColorReslutModel>> Execute(ChangerPixelColorCommand request, CancellationToken cancellationToken)
        {
	       return await _pixelServiceCommand.ChangerPixelColor(request.Pixels, request.Color);
        }
    }
}
