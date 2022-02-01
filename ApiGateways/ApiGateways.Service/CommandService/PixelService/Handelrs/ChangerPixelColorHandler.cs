using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using ApiGateways.Domain.Services.Pixels;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class ChangerPixelColorHandler : HandlerBase<ChangerPixelColorCommand, List<ChangePixelColorResponseModel>>
    {
        private readonly IPixelAndGroupService _pixelService;

        public ChangerPixelColorHandler(IPixelAndGroupService pixelService)
        {
	        _pixelService = pixelService;
        }

        protected override async Task<List<ChangePixelColorResponseModel>> Execute(ChangerPixelColorCommand request, CancellationToken cancellationToken)
        {
	       return await _pixelService.ChangerPixelColor(request.PixelsId, request.Color);
        }
    }
}
