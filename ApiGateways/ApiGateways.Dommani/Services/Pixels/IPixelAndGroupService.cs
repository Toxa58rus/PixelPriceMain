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
	    Task<IResultWithError<GetGroupResponse>> GetGroupByUserId();
        Task<IResultWithError<GetGroupResponse>> GetGroupById(Guid groupId);
	    Task<IResultWithError<GetPixelByGroupIdResponse>> GetPixelByGroupId(Guid groupId);
	    Task<IResultWithError<GetPixelPartResponse>> GetPixelPart(int startPositionX, int startPositionY, int endPositionX, int endPositionY);
        Task<IResultWithError<Guid>> CreateUserPixelGroup(string name, bool isDefault = false);
        Task<IResultWithError> RemovePixelGroup( Guid groupId);
        Task<IResultWithError> ChangePixelGroup(List<Guid> pixels, Guid groupId);
        Task<IResultWithError<List<ChangePixelColorResponse>>> ChangerPixelColor(List<PixelData> pixels);
        Task<IResultWithError<ChangeGroupResponse>> ChangeGroup(string message, string name, Guid groupId);
    }
}
