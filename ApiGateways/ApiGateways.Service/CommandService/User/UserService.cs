﻿using System;
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
