using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Pixels;
using Common.Models.Pixels;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class ChangerPixelGroupOwnerHandler : HandlerBase<ChangerPixelGroupOwnerCommand, List<PixelGroup>>
    {
        private readonly IPixelServiceCommand _pixelServiceCommand;

        public ChangerPixelGroupOwnerHandler(IPixelServiceCommand pixelServiceCommand)
        {
            _pixelServiceCommand = pixelServiceCommand;
        }

        protected override async Task<List<PixelGroup>> Execute(ChangerPixelGroupOwnerCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelServiceCommand.ChangerPixelGroupOwner(request.Groups, request.UserId);
        }
    }
}
