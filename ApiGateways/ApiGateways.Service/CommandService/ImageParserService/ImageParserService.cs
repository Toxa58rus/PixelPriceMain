using Common.Extensions;
using Common.Models;
using Common.Models.ImageParser;
using Common.Rcp;
using Common.Rcp.Client;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Handler.ImageParser;

namespace ApiGateways.Service.CommandService.ImageParserService
{
    public class ImageParserService : IImageParserServiceCommand
    {
        private readonly RpcClient _rpcClient;

        public ImageParserService(IConfiguration configuration)
        {
            var query = configuration["RpcServer:Querys:ImageParser"];
            _rpcClient = new RpcClient(new RpcOptions(query));
        }

        public async Task<ImageData> ParseImage(ImageData data)
        {
            var command = new CommandResponse
            {
                CommandName = "ParseImage",
                Value = data
            };
            return await _rpcClient.SendCommandToServer<ImageData>(command);
        }

    }
}
