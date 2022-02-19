using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Mapping;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using ApiGateways.Domain.Services.Pixels;
using Common.Errors;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;
using MassTransit.Courier;
using MassTransit.Courier.Contracts;

namespace ApiGateways.Service.CommandService.PixelService
{
    public class PixelService : IPixelAndGroupService
    {
	    private readonly IClientFactory _clientFactory;
	    private readonly IBusControl _busControl;
	    private readonly IPublishEndpoint _publish;

	    public PixelService(
		    IConfiguration configuration, 
		    IClientFactory clientFactory, 
		    IBusControl busControl
			)
	    {
		    _clientFactory = clientFactory;
		    _busControl = busControl;
	    }

        public async Task<ResultWithError<Guid>> CreateUserPixelGroup(Guid userId, string name, bool isDefault = false)
        {
	        var requestClient = _clientFactory.CreateRequestClient<CreatePixelGroupRequestDto>();

	        var response = await requestClient.GetResponse<ResultWithError<CreatePixelGroupResponseDto>>(
		        new CreatePixelGroupRequestDto()
		        {
			        Name = name,
			        UserId = userId,
			        IsDefault = isDefault
		        });

			
			return new ResultWithError<Guid>(
				response.Message.ErrorCode, 
				response.Message.Message,
				response.Message.Result.GroupId);
        }

        public async Task<bool> RemovePixelGroup(Guid userId, Guid groupId)
        {

            
            throw new NotImplementedException();
           
        }

        public async Task<List<Pixel>> ChangerPixelGroup(List<Guid> pixels, Guid groupId)
        {

            throw new NotImplementedException();
        }


        public async Task<ResultWithError<List<ChangePixelColorResponse>>> ChangerPixelColor(List<Guid> pixels, int color,Guid userId)
        {
	        var requestClient = _clientFactory.CreateRequestClient<ChangePixelColorRequestDto>();


			var result = await requestClient.GetResponse<ResultWithError<ChangePixelColorResponseDto>>(
				new ChangePixelColorRequestDto()
				{
					Color = color,
					PixelIds = pixels,
					UserId = userId
				});


			return new ResultWithError<List<ChangePixelColorResponse>>(
				result.Message.ErrorCode, 
				result.Message.Message,
				new List<ChangePixelColorResponse>()
				{
					result.Message.Result.ToModel()
				});
			var builder = new RoutingSlipBuilder(NewId.NextGuid());


	        builder.AddActivity("SuperName", new Uri("queue:test"),
		        new
		        {
			        PixelIds = pixels,
			        Color = color,
					UserId = userId,
					GroupId= Guid.Empty
				});

	        //builder.AddSubscription(_busControl.Address, RoutingSlipEvents.All);

			var routingSlip = builder.Build();

			await _busControl.Execute(routingSlip);

			
			//var requestClient = _clientFactory.CreateRequestClient<ChangePixelColorRequestDto>();


			//var result = await requestClient.GetResponse<ResultWithError<ChangePixelColorResponseDto>>(
			//	new ChangePixelColorRequestDto()
			//	{
			//		Color = color,
			//		PixelIds = pixels,
			//                 UserId = userId
			//             });


			//return  new ResultWithError<List<ChangePixelColorResponse>>(
			//	result.Message.ErrorCode,
			//	result.Message.Message,
			//	new List<ChangePixelColorResponse>
			//	{
			//		result.Message.Result?.ToObject()
			//	}
			//); 
        }
    }
}
