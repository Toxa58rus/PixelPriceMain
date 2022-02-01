using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using ApiGateways.Domain.Services.Pixels;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;

namespace ApiGateways.Service.CommandService.PixelService
{
    public class PixelService : IPixelAndGroupService
    {
	    private readonly IClientFactory _clientFactory;

	    public PixelService(
		    IConfiguration configuration, 
		    IClientFactory clientFactory)
	    {
		    _clientFactory = clientFactory;
	    }

        public async Task<PixelGroup> CreateUserPixelGroup(Guid userId, string name, bool isDefault = false)
        {

            /*var command = new CommandResponse
            {
                CommandName = "CreatePixelGroup",
                Value = new PixelGroup(name, userId, isDefault)
            };*/
            // CreateRequestClient
            
            var requestClient = _clientFactory.CreateRequestClient<CreatePixelGroupRequest>();

          /* var result = await requestClient.GetResponse<CreatePixelGroupResponse>(
	           new CreatePixelGroupRequest()
	           {
		           Name = "sdsd", 
		           UserId = Guid.Empty
	           });*/

           //result new PixelGroup(result.Message.Name)
            throw new NotImplementedException();
            //  return await _rpcClient.SendCommandToServer<PixelGroup>(command);

        }

        public async Task<bool> RemovePixelGroup(Guid userId, Guid groupId)
        {
          /*  var command = new CommandResponse
            {
                CommandName = "RemovePixelGroup",
                Value = new RemovePixelGroupResponseModel
                {
                    UserId = userId,
                    GroupId = groupId
                }
            };*/
            throw new NotImplementedException();
            //return await _rpcClient.SendCommandToServer<bool>(command);
        }

        public async Task<List<Pixel>> ChangerPixelGroup(List<Pixel> pixels, Guid groupId)
        {
           /* var command = new CommandResponse
            {
                CommandName = "ChangerPixelGroup",
                Value = new ChangePixelsResponseModel
                {
                    Pixels = pixels,
                    GroupId = groupId
                }
            };*/
            throw new NotImplementedException();
            //return await _rpcClient.SendCommandToServer<List<Pixel>>(command);
        }

        public async Task<List<Pixel>> ChangerPixelsOwner(List<Pixel> pixels, Guid userId)
        {
          /*  var command = new CommandResponse
            {
                CommandName = "ChangerPixelsOwner",
                Value = new ChangePixelsOwnerResponseModel
                {
                    Pixels = pixels,
                    UserId = userId
                }
            };*/
            throw new NotImplementedException();
            //return await _rpcClient.SendCommandToServer<List<Pixel>>(command);

        }

        public async Task<List<PixelGroup>> ChangerPixelGroupOwner(List<PixelGroup> groups, Guid userId)
        {
           /* var command = new CommandResponse
            {
                CommandName = "ChangerPixelGroupOwner",
               // Value = new ChangePixelGroupOwnerResponseModel
               // {
               //     Groups = groups,
               //     UserId = userId
               // }
            };*/
            throw new NotImplementedException();
            // return await _rpcClient.SendCommandToServer<List<PixelGroup>>(command);

        }

        public async Task<List<ChangePixelColorResponseModel>> ChangerPixelColor(List<Guid> pixels, int color)
        {
/*
            var command = new CommandResponse
            {
                CommandName = "ChangerPixelColor",
                //Value = new ChangePixelColorResponseModel
                //{
                //    Pixels = pixels,
                //    Color = color,
                //}
            };*/

			var requestClient = _clientFactory.CreateRequestClient<ChangePixelColorRequest>();

			var result = await requestClient.GetResponse<ChangePixelColorResponse>(
				new ChangePixelColorRequest()
				{
					Color = color,
					PixelIds = pixels
                });

            
			return new List<ChangePixelColorResponseModel>();
			//throw new NotImplementedException();
			//  return await _rpcClient.SendCommandToServer<List<PixelColorReslutModel>>(command);
        }
    }
}
