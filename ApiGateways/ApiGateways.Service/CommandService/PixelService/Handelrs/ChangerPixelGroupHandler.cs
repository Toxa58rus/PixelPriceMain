using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Services.Pixels;
using PixelData = ApiGateways.Domain.Models.PixelsAndGroup.Pixel;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class ChangerPixelGroupHandler : HandlerBase<ChangerPixelGroupCommand, List<PixelData>>
    {
        private readonly IPixelAndGroupService _pixelService;

        public ChangerPixelGroupHandler(IPixelAndGroupService pixelService)
        {
	        _pixelService = pixelService;
        }

        protected override async Task<List<PixelData>> Execute(ChangerPixelGroupCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelService.ChangerPixelGroup(request.Pixels, request.GroupId);
        }
    }
}
