using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Dommain.Handler;
using ApiGateways.Dommain.Handler.Pixels;
using Common.Models.Pixels;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class CreatePixelGroupHandler : HandlerBase<CreatePixelGroupCommand, PixelGroup>
    {
        private readonly IPixelServiceCommand _pixelCommandService;

        public CreatePixelGroupHandler(IPixelServiceCommand pixelCommandService)
        {
            _pixelCommandService = pixelCommandService;
        }

        protected override async Task<PixelGroup> Execute(CreatePixelGroupCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelCommandService.CreateUserPixelGroup(request.UserId, request.Name);
        }
    }
}
