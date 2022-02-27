using System;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.User;
using ApiGateways.Domain.Models.User.Response;
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

	    public UserService(
		    IConfiguration configuration, 
		    IClientFactory clientFactory
	    )
	    {
		    _clientFactory = clientFactory;
	    }


	    public async Task<ResultWithError<SignInDataResponse>> SignIn(SingInCommand request)
	    {
			var requestClient = _clientFactory.CreateRequestClient<SignInUserDataRequestDto>();


			var result = await requestClient.GetResponse<ResultWithError<SignInUserDataResponseDto>>(
				new SignInUserDataRequestDto()
				{
					Login = request.Email,
					Password = request.Password
				});


			return new ResultWithError<SignInDataResponse>(
				result.Message.ErrorCode,
				result.Message.Message,
				new SignInDataResponse()
				{
					Token= result.Message.Result.Token,
					UserId = result.Message.Result.UserId
				});
	    }

	    public async Task<ResultWithError<SignUpDataResponse>> SignUp(SingUpCommand request)
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


			return new ResultWithError<SignUpDataResponse>(
				result.Message.ErrorCode,
				result.Message.Message,
				new SignUpDataResponse()
				{
					Token = result.Message.Result.Token,
					UserId = result.Message.Result.UserId
				});
		}
	}
}
