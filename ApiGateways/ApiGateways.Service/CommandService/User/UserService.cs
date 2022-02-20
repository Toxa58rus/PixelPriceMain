using System;
using System.Threading.Tasks;
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
	    private readonly IBusControl _busControl;
	    private readonly IPublishEndpoint _publish;

	    public UserService(
		    IConfiguration configuration, 
		    IClientFactory clientFactory, 
		    IBusControl busControl
			)
	    {
		    _clientFactory = clientFactory;
		    _busControl = busControl;
	    }


	    public async Task<ResultWithError<SignInDataResponse>> SignIn(string login, string password)
	    {
			var requestClient = _clientFactory.CreateRequestClient<SignInUserDataRequestDto>();


			var result = await requestClient.GetResponse<ResultWithError<SignInUserDataResponseDto>>(
				new SignInUserDataRequestDto()
				{
					Login = login,
					Password = password
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

	    public async Task<ResultWithError<SignUpDataResponse>> SignUp(string login, string password)
	    {
		    var requestClient = _clientFactory.CreateRequestClient<SignUpUserDataRequestDto>();

			var result = await requestClient.GetResponse<ResultWithError<SignUpUserDataResponseDto>>(
				new SignUpUserDataRequestDto()
				{
					Login = login,
					Password = password
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
