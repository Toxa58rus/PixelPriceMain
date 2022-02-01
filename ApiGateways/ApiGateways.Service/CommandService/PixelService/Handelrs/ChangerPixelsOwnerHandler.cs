using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Services.Pixels;
using PixelData = ApiGateways.Domain.Models.PixelsAndGroup.Pixel;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class ChangerPixelsOwnerHandler : HandlerBase<ChangerPixelsOwnerCommand, List<PixelData>>
    {
        private readonly IPixelAndGroupService _pixelService;

        public ChangerPixelsOwnerHandler(IPixelAndGroupService pixelService)
        {
            _pixelService = pixelService;
        }

        protected override async Task<List<PixelData>> Execute(ChangerPixelsOwnerCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelService.ChangerPixelsOwner(request.Pixels, request.UserId);
        }
    }
}
