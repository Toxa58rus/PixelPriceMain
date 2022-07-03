using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Services.Pixels;
using Common.Errors;

namespace ApiGateways.Service.CommandService.PixelService.Handlers
{
    public class RemovePixelGroupHandler : HandlerBase<RemovePixelGroupCommand, IResultWithError>//ResultWithError>
    {
        private readonly IPixelAndGroupService _pixelService;

        public RemovePixelGroupHandler(IPixelAndGroupService pixelService)
        {
            _pixelService = pixelService;
        }

        protected override async Task<IResultWithError> Execute(RemovePixelGroupCommand request, CancellationToken cancellationToken)
        {
	       return await _pixelService.RemovePixelGroup(request.UserId, request.GroupId);
        }
    }
}
