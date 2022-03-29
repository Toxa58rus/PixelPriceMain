using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateways.Domain.Models.PixelsAndGroup;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Common.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateways.Domain.Services.Pixels
{
    public interface IPixelAndGroupService
    {
	    Task<ResultWithError<GetPixelPartResponse>> GetPixelPart(int startPositionX, int startPositionY, int endPositionX, int endPositionY);
        Task<ResultWithError<Guid>> CreateUserPixelGroup(Guid userId, string name, bool isDefault = false);
        Task<bool> RemovePixelGroup(Guid id, Guid groupId);
        Task<List<Pixel>> ChangerPixelGroup(List<Guid> pixels, Guid groupId);
        Task<ResultWithError<List<ChangePixelColorResponse>>> ChangerPixelColor(List<Guid> pixels, int color, Guid userId);
    }
}
