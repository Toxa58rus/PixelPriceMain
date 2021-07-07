using Common.Extensions;
using Common.Models;
using Common.Models.Pixels;
using Common.Rcp;
using Common.Rcp.Client;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.Pixel
{
    public class PixelServiceCommand : IPixelServiceCommand
    {
        private readonly RpcClient _rpcClient;

        public PixelServiceCommand(IConfiguration configuration)
        {
            var query = configuration["RpcServer:Querys:Pixel"];
            _rpcClient = new RpcClient(new RpcOptions(query));
        }

        public async Task<PixelGroup> CreateUserPixelGroup(string userId, string name, bool isDefault = false)
        {
            var command = new CommandResponse
            {
                CommandName = "CreatePixelGroup",
                Value = new PixelGroup(name, userId, isDefault)
            };

            return await SendCommandToServer<PixelGroup>(command);
        }

        public async Task<bool> RemovePixelGroup(string userId, string groupId)
        {
            var command = new CommandResponse
            {
                CommandName = "RemovePixelGroup",
                Value = new RemovePixelGroupResponseModel
                {
                    UserId = userId,
                    GroupId = groupId
                }
            };

            return await SendCommandToServer<bool>(command);
        }

        public async Task<List<Pixels>> ChangerPixelGroup(List<Pixels> pixels, string groupId)
        {
            var command = new CommandResponse
            {
                CommandName = "ChangerPixelGroup",
                Value = new ChangePixelsResponseModel
                {
                    Pixels = pixels,
                    GroupId = groupId
                }
            };

            return await SendCommandToServer<List<Pixels>>(command);
        }

        public async Task<List<Pixels>> ChangerPixelsOwner(List<Pixels> pixels, string userId)
        {
            var command = new CommandResponse
            {
                CommandName = "ChangerPixelsOwner",
                Value = new ChangePixelsOwnerResponseModel
                {
                    Pixels = pixels,
                    UserId = userId
                }
            };
            return await SendCommandToServer<List<Pixels>>(command);
        }

        public async Task<List<PixelGroup>> ChangerPixelGroupOwner(List<PixelGroup> groups, string userId)
        {
            var command = new CommandResponse
            {
                CommandName = "ChangerPixelGroupOwner",
                Value = new ChangePixelGroupOwnerResponseModel
                {
                    Groups = groups,
                    UserId = userId
                }
            };
            return await SendCommandToServer<List<PixelGroup>>(command);
        }

        public async Task<List<PixelColorReslutModel>> ChangerPixelColor(List<Pixels> pixels, string color)
        {
            var command = new CommandResponse
            {
                CommandName = "ChangerPixelColor",
                Value = new ChangePixelColorResponseModel
                {
                    Pixels = pixels,
                    Color = color,
                }
            };
            return await SendCommandToServer<List<PixelColorReslutModel>>(command);
        }

        private async Task<T> SendCommandToServer<T>(CommandResponse command)
        {
            var response = await _rpcClient.CallAsync(command.ToJson(), CancellationToken.None);
            _rpcClient.Dispose();

            return response.DeserializeToObject<T>();
        }
    }
}
