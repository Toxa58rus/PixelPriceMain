using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup;
using Common.Errors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace ApiGateways.Service.CommandService.PixelService.Handlers
{
    public class GetAllPixelsHandler : HandlerBase<GetAllPixelsCommand, IResultWithError<List<Pixel>>>// List<PixelsData>>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetAllPixelsHandler> _logger;

        public GetAllPixelsHandler(IConfiguration configuration, ILogger<GetAllPixelsHandler> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected override Task<IResultWithError<List<Pixel>>> Execute(GetAllPixelsCommand request, CancellationToken cancellationToken)
        {
	        throw new System.NotImplementedException();
        }
    }
}
