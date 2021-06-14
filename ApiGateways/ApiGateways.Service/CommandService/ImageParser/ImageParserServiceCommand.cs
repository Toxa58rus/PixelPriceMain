using Common.Extensions;
using Common.Models;
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

        public async Task<string> ParceImagetoBitmap(string baseString)
        {
            var command = new CommandResponce
            {
                CommandName = "ParceImageToBitmap",
                Value = baseString
            };
            return await SendCommandToServer<string>(command);
        }

        private async Task<T> SendCommandToServer<T>(CommandResponce command)
        {
            var responce = await _rpcClient.CallAsync(command.ToJson(), CancellationToken.None);
            _rpcClient.Dispose();

            return responce.DeserializeToObject<T>();
        }
    }
}
