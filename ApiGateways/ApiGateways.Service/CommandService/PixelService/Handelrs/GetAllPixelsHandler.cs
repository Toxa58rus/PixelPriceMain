using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Command.Pixels;
using ApiGateways.Dommain.Handler;
using Common.Rcp;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PixelsData = Common.Models.Pixels.Pixel;

namespace ApiGateways.Service.CommandService.PixelService.Handelrs
{
    public class GetAllPixelsHandler : HandlerBase<GetAllPixelsCommand, List<PixelsData>>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetAllPixelsHandler> _logger;

        public GetAllPixelsHandler(IConfiguration configuration, ILogger<GetAllPixelsHandler> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected override Task<List<PixelsData>> Execute(GetAllPixelsCommand request, CancellationToken cancellationToken)
        {
	        throw new System.NotImplementedException();
        }
    }
}
