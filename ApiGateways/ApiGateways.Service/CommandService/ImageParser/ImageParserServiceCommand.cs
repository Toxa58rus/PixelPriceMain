using Common.Extensions;
using Common.Models;
using Common.Models.ImageParser;
using Common.Rcp;
using Common.Rcp.Client;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.ImageParser
{
    public class ImageParserServiceCommand : IImageParserServiceCommand
    {
        private readonly RpcClient _rpcClient;

        public ImageParserServiceCommand(IConfiguration configuration)
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
            return await SendCommandToServer<ImageData>(command);
        }

        private async Task<T> SendCommandToServer<T>(CommandResponse command)
        {
            var response = await _rpcClient.CallAsync(command.ToJson(), CancellationToken.None);
            _rpcClient.Dispose();

            return response.DeserializeToObject<T>();
        }
    }
}
