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
    public class GetGroupByUserIdHandler : HandlerBase<GetGroupByUserIdCommand, IResultWithError<GetGroupResponse>>//ResultWithError<GetGroupResponse>>
    {
        private readonly IPixelAndGroupService _pixelService;

        public GetGroupByUserIdHandler(IPixelAndGroupService pixelService)
        {
	        _pixelService = pixelService;
        }

        protected override async Task<IResultWithError<GetGroupResponse>> Execute(GetGroupByUserIdCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelService.GetGroupByUserId();
        }
    }
}
