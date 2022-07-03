using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using ApiGateways.Domain.Services.Pixels;
using Common.Errors;

namespace ApiGateways.Service.CommandService.PixelService.Handlers
{
    public class ChangerPixelColorHandler : HandlerBase<ChangerPixelColorCommand, IResultWithError<List<ChangePixelColorResponse>>>// ResultWithError<List<ChangePixelColorResponse>>>
    {
        private readonly IPixelAndGroupService _pixelService;

        public ChangerPixelColorHandler(IPixelAndGroupService pixelService)
        {
	        _pixelService = pixelService;
        }

        protected override async Task<IResultWithError<List<ChangePixelColorResponse>>> Execute(ChangerPixelColorCommand request, CancellationToken cancellationToken)
        {
	       return await _pixelService.ChangerPixelColor(request.PixelsId, request.Color,request.UserId);
        }
    }
}
