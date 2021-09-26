using Common.Extensions;
using Common.Models;
using Common.Models.Pixels;
using Common.Rcp;
using Common.Rcp.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Dommain.Handler.Pixels;

namespace ApiGateways.Service.CommandService.PixelService
{
    public class PixelService : IPixelServiceCommand
    {
        private readonly RpcClient _rpcClient;

        public PixelService(IConfiguration configuration)
        {


        }

        public async Task<PixelGroup> CreateUserPixelGroup(Guid userId, string name, bool isDefault = false)
        {
            var command = new CommandResponse
            {
                CommandName = "CreatePixelGroup",
                Value = new PixelGroup(name, userId, isDefault)
            };
            return await _rpcClient.SendCommandToServer<PixelGroup>(command);

        }

        public async Task<bool> RemovePixelGroup(Guid userId, Guid groupId)
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

            return await _rpcClient.SendCommandToServer<bool>(command);
        }

        public async Task<List<Pixel>> ChangerPixelGroup(List<Pixel> pixels, Guid groupId)
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

            return await _rpcClient.SendCommandToServer<List<Pixel>>(command);
        }

        public async Task<List<Pixel>> ChangerPixelsOwner(List<Pixel> pixels, Guid userId)
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

            return await _rpcClient.SendCommandToServer<List<Pixel>>(command);

        }

        public async Task<List<PixelGroup>> ChangerPixelGroupOwner(List<PixelGroup> groups, Guid userId)
        {
            var command = new CommandResponse
            {
                CommandName = "ChangerPixelGroupOwner",
               // Value = new ChangePixelGroupOwnerResponseModel
               // {
               //     Groups = groups,
               //     UserId = userId
               // }
            };
            return await _rpcClient.SendCommandToServer<List<PixelGroup>>(command);

        }

        public async Task<List<PixelColorReslutModel>> ChangerPixelColor(List<Pixel> pixels, int color)
        {
            var command = new CommandResponse
            {
                CommandName = "ChangerPixelColor",
                //Value = new ChangePixelColorResponseModel
                //{
                //    Pixels = pixels,
                //    Color = color,
                //}
            };

            return await _rpcClient.SendCommandToServer<List<PixelColorReslutModel>>(command);
        }
    }
}
