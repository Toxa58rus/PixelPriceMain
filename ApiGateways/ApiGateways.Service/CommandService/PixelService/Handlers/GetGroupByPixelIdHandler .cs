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
    public class GetGroupByPixelIdHandler : HandlerBase<GetGroupByPixelIdCommand, IResultWithError<GetGroupResponse>>//ResultWithError<GetGroupResponse>>
    {
        private readonly IPixelAndGroupService _pixelService;

        public GetGroupByPixelIdHandler(IPixelAndGroupService pixelService)
        {
	        _pixelService = pixelService;
        }

        protected override async Task<IResultWithError<GetGroupResponse>> Execute(GetGroupByPixelIdCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelService.GetGroupByPixelId(request.PixelId);
        }
    }
}
