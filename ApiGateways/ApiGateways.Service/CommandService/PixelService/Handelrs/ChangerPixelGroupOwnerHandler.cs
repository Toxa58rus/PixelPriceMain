using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Services.Pixels;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class ChangerPixelGroupOwnerHandler : HandlerBase<ChangerPixelGroupOwnerCommand, List<PixelGroup>>
    {
        private readonly IPixelAndGroupService _pixelService;

        public ChangerPixelGroupOwnerHandler(IPixelAndGroupService pixelService)
        {
            _pixelService = pixelService;
        }

        protected override async Task<List<PixelGroup>> Execute(ChangerPixelGroupOwnerCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelService.ChangerPixelGroupOwner(request.Groups, request.UserId);
        }
    }
}
