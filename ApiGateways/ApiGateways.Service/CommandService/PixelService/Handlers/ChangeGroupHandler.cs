using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using ApiGateways.Domain.Services.Pixels;
using Common.Errors;
using PixelData = ApiGateways.Domain.Models.PixelsAndGroup.Pixel;

namespace ApiGateways.Service.CommandService.PixelService.Handlers
{
    public class ChangeGroupHandler : HandlerBase<ChangerGroupCommand, IResultWithError<ChangeGroupResponse>> //ResultWithError<ChangeGroupResponse>>
    {
        private readonly IPixelAndGroupService _pixelService;

        public ChangeGroupHandler(IPixelAndGroupService pixelService)
        {
	        _pixelService = pixelService;
        }

        protected override async Task<IResultWithError<ChangeGroupResponse>> Execute(ChangerGroupCommand request, CancellationToken cancellationToken)
        {

	         return await _pixelService.ChangeGroup(request.Massage, request.UserId,request.Name,request.GroupId);
        }
    }
}
