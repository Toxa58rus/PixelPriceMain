using Common.Extensions;
using Common.Models;
using Common.Models.Pixels;
using Common.Rcp;
using Common.Rcp.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.Pixel
{
    public class PixelServiceCommand : IPixelServiceCommand
    {
        private readonly RpcClient _rpcClient;

        public PixelServiceCommand()
        {
            _rpcClient = new RpcClient(new RpcOptions("Pixel"));
        }

        public async Task<PixelGroup> CreateUserPixelGroup(string userId, string name, bool isDefault = false)
        {
            var command = new CommandResponce
            {
                CommandName = "CreatePixelGroup",
                Value = new PixelGroup(name, userId, isDefault)
            };

            return await SendCommandToServer<PixelGroup>(command);
        }

        public async Task<bool> RemovePixelGroup(string userId, string groupId)
        {
            var command = new CommandResponce
            {
                CommandName = "RemovePixelGroup",
                Value = new RemovePixelGroupResponceModel
                {
                    UserId = userId,
                    GroupId = groupId
                }
            };

            return await SendCommandToServer<bool>(command);
        }

        public async Task<List<Pixels>> ChangerPixelGroup(List<Pixels> pixels, string groupId)
        {
            var command = new CommandResponce
            {
                CommandName = "ChangerPixelGroup",
                Value = new ChangePixelsResponceModel
                {
                    Pixels = pixels,
                    GroupId = groupId
                }
            };

            return await SendCommandToServer<List<Pixels>>(command);
        }

        public async Task<string> ChangerPixelsOwner(List<Pixels> pixels, string userId)
        {
            var command = new CommandResponce
            {
                CommandName = "ChangerPixelsOwner",
                Value = new ChangePixelsOwnerResponceModel
                {
                    Pixels = pixels,
                    UserId = userId
                }
            };
            return await SendCommandToServer<string>(command);
        }

        public async Task<string> ChangerPixelGroupOwner(List<PixelGroup> groups, string userId)
        {
            var command = new CommandResponce
            {
                CommandName = "ChangerPixelGroupOwner",
                Value = new ChangePixelGroupOwnerResponceModel
                {
                    Groups = groups,
                    UserId = userId
                }
            };
            return await SendCommandToServer<string>(command);
        }

        public async Task<PixelColor> ChangerPixelColor(List<Pixels> pixels, string color)
        {
            var command = new CommandResponce
            {
                CommandName = "ChangerPixelColor",
                Value = new ChangePixelColorResponceModel
                {
                    Pixels = pixels,
                    Color = color,
                }
            };
            return await SendCommandToServer<PixelColor>(command);
        }


        private async Task<T> SendCommandToServer<T>(CommandResponce command)
        {
            var responce = await _rpcClient.CallAsync(command.ToJson(), CancellationToken.None);
            _rpcClient.Dispose();

            return responce.DeserializeToObject<T>();
        }
    }
}
