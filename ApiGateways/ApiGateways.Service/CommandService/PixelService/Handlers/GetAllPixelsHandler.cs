using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PixelsData = ApiGateways.Domain.Models.PixelsAndGroup.Pixel;

namespace ApiGateways.Service.CommandService.PixelService.Handlers
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
