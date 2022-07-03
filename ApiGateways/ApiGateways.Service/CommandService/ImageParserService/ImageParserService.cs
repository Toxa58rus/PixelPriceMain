using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Domain.Models.Image.Response;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using ApiGateways.Domain.Services.ImageParser;
using Common.Errors;
using Contracts.ImageParserContract.ImageParserRequest;
using Contracts.ImageParserContract.ImageParserResponse;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;

namespace ApiGateways.Service.CommandService.ImageParserService
{
    public class ImageParserService : IImageParserService
    {
	    private readonly IClientFactory _clientFactory;

	    private readonly IBusControl _busControl;


        public ImageParserService(
	        IClientFactory clientFactory,
	        IBusControl busControl)
        {
	        _clientFactory = clientFactory;
	        _busControl = busControl;
        }

        public async Task<IResultWithError<ImageDataResponse>> SetImageForGroup(string imageBaseString, Guid groupId)
        {
			var requestClient = _clientFactory.CreateRequestClient<SetImageInGroupRequestDto>();

			var response = await requestClient.GetResponse<ResultWithError<SetImageInGroupResponseDto>>(
				new SetImageInGroupRequestDto()
				{
					ImageBaseString = imageBaseString,
					GroupId = groupId
				});

			if (response.Message.IsError)
			{
				return new ResultWithError<ImageDataResponse>(
					response.Message.ErrorCode,
					response.Message.Message,
					null);
			}

			return new ResultWithError<ImageDataResponse>(
				response.Message.ErrorCode,
				response.Message.Message,
				new ImageDataResponse()
				{
					GroupId = response.Message.Result.GroupId,
					ImageBaseString = response.Message.Result.ImageBaseString
				});
        }
    }
}
