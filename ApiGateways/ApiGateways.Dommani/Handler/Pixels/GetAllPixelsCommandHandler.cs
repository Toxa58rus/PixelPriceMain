using ApiGateways.Dommain.Command.Pixels;
using Common.Rcp;
using Common.Rcp.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Extensions;
using PixelsData = Common.Models.Pixels.Pixels;
using Common.Models;

namespace ApiGateways.Dommain.Handler.Pixels
{
    public class GetAllPixelsCommandHandler : IRequestHandler<GetAllPixelsCommand, List<PixelsData>>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetAllPixelsCommandHandler> _logger;

        public GetAllPixelsCommandHandler(IConfiguration configuration, ILogger<GetAllPixelsCommandHandler> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<PixelsData>> Handle(GetAllPixelsCommand request, CancellationToken cancellationToken)
        {
            var options = new RpcOptions("Pixel");

            using (var rpcClient = new RpcClient(options))
            {
                _logger.LogInformation("get all pixel");

                var command = new CommandResponce
                {
                    CommandName = "GetAllPixelsCommand",
                    Value = null
                };

                var responce = await rpcClient.CallAsync(command.ToJson(), cancellationToken);
                return responce.DeserializeToObject<List<PixelsData>>();
            }
        }
    }
}
