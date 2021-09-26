using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Pixels;
using MediatR;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class RemovePixelGrpupHandler : HandlerBase<RemovePixeGrpupCommand, bool>
    {
        private readonly IPixelServiceCommand _pixelService;

        public RemovePixelGrpupHandler(IPixelServiceCommand pixelService)
        {
            _pixelService = pixelService;
        }

        protected override async Task<bool> Execute(RemovePixeGrpupCommand request, CancellationToken cancellationToken)
        {
	       return await _pixelService.RemovePixelGroup(request.Id, request.GroupId);
        }
    }
}
