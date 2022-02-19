using System;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain.Models.ImageParser;
using ApiGateways.Domain.Services.ImageParser;
using Contracts.MailContract.MailRequest;
using MassTransit;

namespace ApiGateways.Service.CommandService.ImageParserService
{
    public class ImageParserService : IImageParserService
    {
       // private readonly RpcClient _rpcClient;

        public ImageParserService(IConfiguration configuration, IRequestClient<SendMailRequestDto> requestClien)
        {
            var query = configuration["RpcServer:Querys:ImageParser"];
            //_rpcClient = new RpcClient(new RpcOptions(query));
        }

        public async Task<ImageData> ParseImage(ImageData data)
        {
	        throw new NotImplementedException();
            //  return await _rpcClient.SendCommandToServer<ImageData>(command);
        }

    }
}
