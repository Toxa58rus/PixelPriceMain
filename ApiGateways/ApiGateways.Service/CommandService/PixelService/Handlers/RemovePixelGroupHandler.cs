using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Services.Pixels;

namespace ApiGateways.Service.CommandService.PixelService.Handlers
{
    public class RemovePixelGroupHandler : HandlerBase<RemovePixelGroupCommand, bool>
    {
        private readonly IPixelAndGroupService _pixelService;

        public RemovePixelGroupHandler(IPixelAndGroupService pixelService)
        {
            _pixelService = pixelService;
        }

        protected override async Task<bool> Execute(RemovePixelGroupCommand request, CancellationToken cancellationToken)
        {
	       return await _pixelService.RemovePixelGroup(request.Id, request.GroupId);
        }
    }
}
