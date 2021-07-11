﻿using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Service.CommandService.Pixel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Handler.Pixels
{
    public class RemovePixelGrpupCommandHandler : IRequestHandler<RemovePixeGrpupCommand, bool>
    {
        private readonly IPixelServiceCommand _pixelService;

        public RemovePixelGrpupCommandHandler(IPixelServiceCommand pixelService)
        {
            _pixelService = pixelService;
        }
        
        public async Task<bool> Handle(RemovePixeGrpupCommand request, CancellationToken cancellationToken) =>
            await _pixelService.RemovePixelGroup(request.Id, request.GroupId);
    }
}