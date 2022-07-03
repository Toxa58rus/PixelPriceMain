using System;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Services.Pixels;
using Common.Errors;

namespace ApiGateways.Service.CommandService.PixelService.Handlers
{
    public class CreatePixelGroupHandler : HandlerBase<CreatePixelGroupCommand, IResultWithError<Guid>>
    {
        private readonly IPixelAndGroupService _pixelService;

        public CreatePixelGroupHandler(IPixelAndGroupService pixelService)
        {
	        _pixelService = pixelService;
        }

        protected override async Task<IResultWithError<Guid>> Execute(CreatePixelGroupCommand request, CancellationToken cancellationToken)
        {
	        return await _pixelService.CreateUserPixelGroup(request.UserId, request.Name);
        }
    }
}
