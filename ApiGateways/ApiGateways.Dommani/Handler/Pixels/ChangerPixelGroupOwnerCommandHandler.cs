using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Service.CommandService.Pixel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Pixels
{
    public class ChangerPixelGroupOwnerCommandHandler : IRequestHandler<ChangerPixelGroupOwnerCommand, string>
    {
        private readonly IPixelServiceCommand _pixelServiceCommand;

        public ChangerPixelGroupOwnerCommandHandler(IPixelServiceCommand pixelServiceCommand)
        {
            _pixelServiceCommand = pixelServiceCommand;
        }

        public async Task<string> Handle(ChangerPixelGroupOwnerCommand request, CancellationToken cancellationToken) =>
            await _pixelServiceCommand.ChangerPixelGroupOwner(request.Groups, request.UserId);
    }
}
