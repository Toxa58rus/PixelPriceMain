using System;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.User;
using ApiGateways.Domain.Models.User.Response;
using ApiGateways.Domain.Services;
using ApiGateways.Domain.Services.User;
using Common.Errors;
using Contracts.UserContract.UserRequest;
using Contracts.UserContract.UserResponse;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace ApiGateways.Service.CommandService.User
{
    public class UserService : IUserService
	{
	    private readonly IClientFactory _clientFactory;
	    private readonly UserContext _userContext;
		public UserService(
		    IConfiguration configuration, 
		    IClientFactory clientFactory, 
		    UserContext userContext)
		{
			_clientFactory = clientFactory;
			_userContext = userContext;
		}

		public async Task<IResultWithError<GetUserInfoResponse>> GetUserInfo(GetUserInfoCommand request)
		{
			var requestClient = _clientFactory.CreateRequestClient<GetUserInfoRequestDto>();

			var result = await requestClient.GetResponse<ResultWithError<GetUserInfoResponseDto>>(
				new GetUserInfoRequestDto()
				{
					UserId = request.UserId == Guid.Empty ? _userContext.UserId : request.UserId
				});

			if (result.Message.IsError)
			{
				return new ResultWithError<GetUserInfoResponse>(
					result.Message.ErrorCode,
					result.Message.Message,
					null);
			}

			return new ResultWithError<GetUserInfoResponse>(
				result.Message.ErrorCode,
				result.Message.Message,
				new GetUserInfoResponse()
				{
					Name=result.Message.Result.Name
				}
			);
		}

		public async Task<IResultWithError> SetImage(SetImageCommand request)
	    {
		    var requestClient = _clientFactory.CreateRequestClient<SetImageUserRequestDto>();


		    var result = await requestClient.GetResponse<ResultWithError>(
			    new SetImageUserRequestDto()
			    {
				    UserId = request.UserId == Guid.Empty ? _userContext.UserId : request.UserId,
				    ImageBaseString = request.Image
			    });

		    if (result.Message.IsError)
		    {
			    return new ResultWithError(
				    result.Message.ErrorCode,
				    result.Message.Message
				    );
		    }

		    return new ResultWithError(
			    result.Message.ErrorCode,
			    result.Message.Message
			    );
	    }
	    public async Task<IResultWithError<string>> GetImage(GetImageCommand request)
	    {
		    var requestClient = _clientFactory.CreateRequestClient<GetImageUserRequestDto>();

		    var getImageUserRequestDto = new GetImageUserRequestDto()
		    {
			    UserId = request.UserId == Guid.Empty ? _userContext.UserId : request.UserId
		    };

			var result = await requestClient.GetResponse<ResultWithError<GetImageUserResponseDto>>(
				getImageUserRequestDto);

		    if (result.Message.IsError)
		    {
			    return new ResultWithError<string>(
					    result.Message.ErrorCode,
					    result.Message.Message,
					    null
					    );
		    }

		    return new ResultWithError<string>(
			    result.Message.ErrorCode,
			    result.Message.Message,
			    result.Message.Result.ImageBaseString
			);
	    }
		public async Task<IResultWithError<SignInDataResponse>> SignIn(SingInCommand request)
	    {
			var requestClient = _clientFactory.CreateRequestClient<SignInUserDataRequestDto>();


			var result = await requestClient.GetResponse<ResultWithError<SignInUserDataResponseDto>>(
				new SignInUserDataRequestDto()
				{
					Login = request.Email,
					Password = request.Password,
					RefreshToken = request.RefreshToken
				});

			if (result.Message.IsError)
			{
				return new ResultWithError<SignInDataResponse>(
					result.Message.ErrorCode,
					result.Message.Message,
					null);
			}

			return new ResultWithError<SignInDataResponse>(
				result.Message.ErrorCode,
				result.Message.Message,
				new SignInDataResponse()
				{
					AccessToken = result.Message.Result.AccessToken,
					UserId = result.Message.Result.UserId,
					RefreshToken = result.Message.Result.RefreshToken
				});
	    }

	    public async Task<IResultWithError<SignUpDataResponse>> SignUp(SingUpCommand request)
	    {
		    var requestClient = _clientFactory.CreateRequestClient<SignUpUserDataRequestDto>();

		    var result = await requestClient.GetResponse<ResultWithError<SignUpUserDataResponseDto>>(
			    new SignUpUserDataRequestDto()
			    {
				    Login = request.Email,
				    Password = request.Password,
				    ConfirmPassword = request.ConfirmPassword,
				    UserName = request.UserName
			    });


		    if (result.Message.IsError)
		    {
			    return new ResultWithError<SignUpDataResponse>(
				    result.Message.ErrorCode,
				    result.Message.Message,
				    null);
				
		    }
		    else
		    {
			    return new ResultWithError<SignUpDataResponse>(
				    result.Message.ErrorCode,
				    result.Message.Message,
				    new SignUpDataResponse()
				    {
					    AccessToken = result.Message.Result.AccessToken,
					    UserId = result.Message.Result.UserId,
					    RefreshToken = result.Message.Result.RefreshToken
				    });

			}
	    }
	}
}
