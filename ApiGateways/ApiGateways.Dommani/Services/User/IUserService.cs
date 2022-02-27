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
        Task<ResultWithError<SignInDataResponse>> SignIn(SingInCommand request);
        Task<ResultWithError<SignUpDataResponse>> SignUp(SingUpCommand request);

    }
}
