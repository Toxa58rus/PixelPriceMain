using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Services.Pixels;
using Common.Errors;
using PixelData = ApiGateways.Domain.Models.PixelsAndGroup.Pixel;

namespace ApiGateways.Service.CommandService.PixelService.Handlers
{
    public class ChangePixelGroupHandler : HandlerBase<ChangerPixelGroupCommand, IResultWithError>// ResultWithError>
    {
        private readonly IPixelAndGroupService _pixelService;

        public ChangePixelGroupHandler(IPixelAndGroupService pixelService)
        {
	        _pixelService = pixelService;
        }

        protected override async Task<IResultWithError> Execute(ChangerPixelGroupCommand request, CancellationToken cancellationToken)
        {

	         return await _pixelService.ChangePixelGroup(request.Pixels, request.GroupId);
        }
    }
}
