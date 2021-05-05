using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Service.CommandService.Pixel;
using Common.Models.Pixels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Pixels
{
    public class CreatePixelGroupCommandHandler : IRequestHandler<CreatePixelGroupCommand, PixelGroup>
    {
        private readonly IPixelServiceCommand _pixelCommandService;

        public CreatePixelGroupCommandHandler(IPixelServiceCommand pixelCommandService)
        {
            _pixelCommandService = pixelCommandService;
        }

        public async Task<PixelGroup> Handle(CreatePixelGroupCommand request, CancellationToken cancellationToken) =>
            await _pixelCommandService.CreateUserPixelGroup(request.UserId, request.Name);
    }
}
