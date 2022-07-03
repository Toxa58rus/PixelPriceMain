using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Common.Errors;
using Contracts.PixelContract.PixelResponse;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateways.Domain.Services.Pixels
{
    public interface IPixelAndGroupService
    {
	    Task<IResultWithError<GetGroupResponse>> /*ResultWithError<GetGroupResponse>>*/ GetGroupByUserId(Guid groupId);
        Task<IResultWithError<GetGroupResponse>>/*ResultWithError<GetGroupResponse>>*/ GetGroupById(Guid groupId);
	    Task<IResultWithError<GetPixelByGroupIdResponse>>/*ResultWithError<GetPixelByGroupIdResponse>> */GetPixelByGroupId(Guid groupId);
	    Task<IResultWithError<GetPixelPartResponse>>/*IResultWithError> */GetPixelPart(int startPositionX, int startPositionY, int endPositionX, int endPositionY);
        Task<IResultWithError<Guid>>/*ResultWithError<Guid>> */CreateUserPixelGroup(Guid userId, string name, bool isDefault = false);
        Task<IResultWithError>/*ResultWithError> */RemovePixelGroup(Guid userId, Guid groupId);
        Task<IResultWithError>/*ResultWithError> */ChangePixelGroup(List<Guid> pixels, Guid groupId);
        Task<IResultWithError<List<ChangePixelColorResponse>>>/*ResultWithError<List<ChangePixelColorResponse>>> */ChangerPixelColor(List<Guid> pixels, int color, Guid userId);
        Task<IResultWithError<ChangeGroupResponse>>/* ResultWithError<ChangeGroupResponse>> */ChangeGroup(string message, Guid userId, string name, Guid groupId);
    }
}
