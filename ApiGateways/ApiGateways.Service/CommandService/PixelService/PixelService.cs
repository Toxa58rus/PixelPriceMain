using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Mapping;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using ApiGateways.Domain.Services;
using ApiGateways.Domain.Services.Pixels;
using Common.Errors;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;

namespace ApiGateways.Service.CommandService.PixelService
{
    public class PixelService : IPixelAndGroupService
    {
	    private readonly IClientFactory _clientFactory;
	    private readonly UserContext _userContext;

	    public PixelService(
		    IConfiguration configuration, 
		    IClientFactory clientFactory,
		    UserContext userContext
			)
	    {
		    _clientFactory = clientFactory;
		    _userContext = userContext;
	    }

	    public async Task<IResultWithError<GetPixelByGroupIdResponse>> GetPixelByGroupId(Guid groupId)
	    {
			var requestClient = _clientFactory.CreateRequestClient<GetPixelByGroupIdRequestDto>();

			var response = await requestClient.GetResponse<ResultWithError<GetPixelByGroupIdResponseDto>>(
				new GetPixelByGroupIdRequestDto()
				{
					GroupId=groupId
				});

			if (response.Message.IsError)
			{
				return new ResultWithError<GetPixelByGroupIdResponse>(
					response.Message.ErrorCode,
					response.Message.Message,
					null);
			}

			var listPixel = response.Message.Result.Pixels.Select(x => new Pixel()
			{
				Color = x.Color,
				GroupId = x.GroupId,
				Id = x.Id,
				UserId = x.UserId,
				X = x.X,
				Y = x.Y
			});

			return new ResultWithError<GetPixelByGroupIdResponse>(
				response.Message.ErrorCode,
				response.Message.Message,
				new GetPixelByGroupIdResponse()
				{
					Pixels = listPixel.ToList()
				});
		}

	    public async Task<IResultWithError<GetPixelPartResponse>> GetPixelPart(int startPositionX, int startPositionY, int endPositionX, int endPositionY)
	    {
			var requestClient = _clientFactory.CreateRequestClient<GetPixelPartRequestDto>();

			var response = await requestClient.GetResponse<ResultWithError<GetPixelPartResponseDto>>(
				new GetPixelPartRequestDto()
				{
					StartPositionX = startPositionX,
					StartPositionY = startPositionY,
					EndPositionX = endPositionX,
					EndPositionY = endPositionY
				});

			var listPixel = response.Message.Result.Pixels.Select(x => new Pixel()
			{
				Color = x.Color,
				GroupId = x.GroupId,
				Id = x.Id,
				UserId = x.UserId,
				X = x.X,
				Y = x.Y
			});

			return new ResultWithError<GetPixelPartResponse>(
				response.Message.ErrorCode,
				response.Message.Message,
				new GetPixelPartResponse()
				{
					Pixels = listPixel.ToList()
				});
	    }

	    public async Task<IResultWithError<Guid>> CreateUserPixelGroup( string name, bool isDefault = false)
        {
	        var requestClient = _clientFactory.CreateRequestClient<CreatePixelGroupRequestDto>();

	        var response = await requestClient.GetResponse<ResultWithError<CreatePixelGroupResponseDto>>(
		        new CreatePixelGroupRequestDto()
		        {
			        Name = name,
			        UserId = _userContext.UserId,
			        IsDefault = isDefault
		        });

	        if (response.Message.IsError)
	        {
		        return new ResultWithError<Guid>(
			        response.Message.ErrorCode,
			        response.Message.Message,
			        Guid.Empty);
			}

	        return new ResultWithError<Guid>(
				response.Message.ErrorCode, 
				response.Message.Message,
				response.Message.Result.GroupId);
        }

        public async Task<IResultWithError> RemovePixelGroup(Guid groupId)
        {
	        var requestClient = _clientFactory.CreateRequestClient<RemovePixelGroupRequestDto>();

			var result = await requestClient.GetResponse<ResultWithError>(
				new RemovePixelGroupRequestDto()
				{
					GroupId = groupId,
					UserId = _userContext.UserId
				});

			return new ResultWithError(
				result.Message.ErrorCode,
				result.Message.Message
			);
        }
        public async Task<IResultWithError<IEnumerable<GetGroupResponse>>> GetGroupByUserId()
        {
	        var requestClient = _clientFactory.CreateRequestClient<GetGroupByUserIdRequestDto>();

	        var result = await requestClient.GetResponse<ResultWithError<IEnumerable<GetGroupResponseDto>>>(
		        new GetGroupByUserIdRequestDto()
		        {
			        UserId = _userContext.UserId
				});

	        if (result.Message.Result == null)
	        {
		        return new ResultWithError<IEnumerable<GetGroupResponse>>(
			        result.Message.ErrorCode,
			        result.Message.Message,
			        null);
	        }

	        return new ResultWithError<IEnumerable<GetGroupResponse>>(
		        result.Message.ErrorCode,
		        result.Message.Message,
		        result.Message.Result.Select(x => new GetGroupResponse()
		        {
			        Message = x.Massage,
			        UserId = x.UserId,
			        Name = x.Name,
			        GroupId = x.GroupId
		        }));

				
        }
		public async Task<IResultWithError<GetGroupResponse>> GetGroupById(Guid groupId)
        {

	        var requestClient = _clientFactory.CreateRequestClient<GetGroupByIdRequestDto>();

	        var result = await requestClient.GetResponse<ResultWithError<GetGroupResponseDto>>(
		        new GetGroupByIdRequestDto()
		        {
			        GroupId = groupId
		        });

	        if (result.Message.Result == null)
	        {
		        return new ResultWithError<GetGroupResponse>(
			        result.Message.ErrorCode,
			        result.Message.Message,
			        null);
	        }

			return new ResultWithError<GetGroupResponse>(
		        result.Message.ErrorCode,
		        result.Message.Message,
		        new GetGroupResponse()
		        {
			        Message = result.Message.Result.Massage,
			        UserId = result.Message.Result.UserId,
			        Name = result.Message.Result.Name,
			        GroupId = groupId
		        });
        }

		public async Task<IResultWithError<GetGroupResponse>> GetGroupByPixelId(Guid pixelId)
		{

			var requestClient = _clientFactory.CreateRequestClient<GetGroupByPixelIdRequestDto>();

			var result = await requestClient.GetResponse<ResultWithError<GetGroupResponseDto>>(
				new GetGroupByPixelIdRequestDto()
				{
					PixelId = pixelId
				});

			if (result.Message.Result == null)
			{
				return new ResultWithError<GetGroupResponse>(
					result.Message.ErrorCode,
					result.Message.Message,
					null);
			}

			return new ResultWithError<GetGroupResponse>(
				result.Message.ErrorCode,
				result.Message.Message,
				new GetGroupResponse()
				{
					Message = result.Message.Result.Massage,
					UserId = result.Message.Result.UserId,
					Name = result.Message.Result.Name,
					GroupId = pixelId
				});
		}
		public async Task<IResultWithError<ChangeGroupResponse>> ChangeGroup(string message, string name, Guid groupId)
        {
	        var requestClient = _clientFactory.CreateRequestClient<ChangeGroupRequestDto>();

	        var result = await requestClient.GetResponse<ResultWithError<ChangeGroupResponseDto>>(
		        new ChangeGroupRequestDto()
		        {
			        GroupId = groupId,
			        Name = name,
			        UserId = _userContext.UserId,
			        Massage = message
		        });

	        if (result.Message.IsError)
	        {
		        return new ResultWithError<ChangeGroupResponse>(
			        result.Message.ErrorCode,
			        result.Message.Message,
			        null);
	        }

			return new ResultWithError<ChangeGroupResponse>(
		        result.Message.ErrorCode,
		        result.Message.Message,
		        new ChangeGroupResponse()
		        {
			        Massage = result.Message.Result.Massage,
			        UserId = result.Message.Result.UserId,
			        Name = result.Message.Result.Name,
			        GroupId = groupId
		        });
        }
		public async Task<IResultWithError> ChangePixelGroup(List<Guid> pixels, Guid groupId)
        {
	        var requestClient = _clientFactory.CreateRequestClient<ChangePixelGroupRequestDto>();

			var result = await requestClient.GetResponse<ResultWithError>(
				new ChangePixelGroupRequestDto()
				{
					GroupId = groupId,
					PixelIds = pixels
				});

			return new ResultWithError(
				result.Message.ErrorCode,
				result.Message.Message
			);
        }

		public async Task<IResultWithError<List<ChangePixelColorResponse>>> ChangerPixelColor(List<PixelData> pixels)
        {
	        var requestClient = _clientFactory.CreateRequestClient<ChangePixelColorRequestDto>();


	        var result = await requestClient.GetResponse<ResultWithError<ChangePixelColorResponseDto>>(
		        new ChangePixelColorRequestDto()
		        {

			        Pixels = pixels.Select(x => new Contracts.PixelContract.PixelDataDto()
			        {
				        Color = x.Color, 
				        Id = x.Id
			        }).ToList(),
			        UserId = _userContext.UserId
				});

	        if (result.Message.IsError)
	        {
		        return new ResultWithError<List<ChangePixelColorResponse>>(
			        result.Message.ErrorCode,
			        result.Message.Message,
			        null);
			}

	        return new ResultWithError<List<ChangePixelColorResponse>>(
				result.Message.ErrorCode, 
				result.Message.Message,
				new List<ChangePixelColorResponse>()
				{
					result.Message.Result?.ToModel()
				});
        }
    }
}
