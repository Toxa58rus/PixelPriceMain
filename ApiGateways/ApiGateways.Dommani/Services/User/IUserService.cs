using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateways.Domain.Command.User;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using ApiGateways.Domain.Models.User.Response;
using Common.Errors;
using Contracts.UserContract.UserResponse;

namespace ApiGateways.Domain.Services.User
{
    public interface IUserService
    {
        Task<IResultWithError<SignInDataResponse>> SignIn(SingInCommand request);
        Task<IResultWithError<SignUpDataResponse>> SignUp(SingUpCommand request);
        Task<IResultWithError> SetImage(SetImageCommand request);
        Task<IResultWithError<string>> GetImage(GetImageCommand request);
        Task<IResultWithError<GetUserInfoResponse>> GetUserInfo(GetUserInfoCommand request);
    }
}
