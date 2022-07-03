using System;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using ApiGateways.Domain.Services.Pixels;
using Common.Errors;

namespace ApiGateways.Service.CommandService.PixelService.Handlers
{
    public class GetPixelByGroupIdHandler : HandlerBase<GetPixelByGroupIdCommand, IResultWithError<GetPixelByGroupIdResponse>>//ResultWithError<GetPixelByGroupIdResponse>>
    {
        private readonly IPixelAndGroupService _pixelService;

        public GetPixelByGroupIdHandler(IPixelAndGroupService pixelService)
        {
	        _pixelService = pixelService;
        }

        protected override async Task<IResultWithError<GetPixelByGroupIdResponse>> Execute(GetPixelByGroupIdCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelService.GetPixelByGroupId(request.GroupId);
        }
    }
}
