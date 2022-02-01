using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Services.Pixels;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class CreatePixelGroupHandler : HandlerBase<CreatePixelGroupCommand, PixelGroup>
    {
        private readonly IPixelAndGroupService _pixelService;

        public CreatePixelGroupHandler(IPixelAndGroupService pixelService)
        {
	        _pixelService = pixelService;
        }

        protected override async Task<PixelGroup> Execute(CreatePixelGroupCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelService.CreateUserPixelGroup(request.UserId, request.Name);
        }
    }
}
