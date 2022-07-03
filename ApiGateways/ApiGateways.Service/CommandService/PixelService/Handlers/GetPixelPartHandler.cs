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
    public class GetPixelPartHandler : HandlerBase<GetPixelPartCommand, IResultWithError<GetPixelPartResponse>>//ResultWithError<GetPixelPartResponse>>
    {
        private readonly IPixelAndGroupService _pixelService;

        public GetPixelPartHandler(IPixelAndGroupService pixelService)
        {
	        _pixelService = pixelService;
        }

        protected override async Task<IResultWithError<GetPixelPartResponse>> Execute(GetPixelPartCommand request, CancellationToken cancellationToken)
        {
	       return await _pixelService.GetPixelPart(request.StartPositionX, request.StartPositionY, request.EndPositionX, request.EndPositionY);
        }
    }
}
