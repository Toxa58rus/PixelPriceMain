using System;
using System.Threading.Tasks;
using ApiGateways.Domain.Models.Image.Response;
using Common.Errors;

namespace ApiGateways.Domain.Services.ImageParser
{
    public interface IImageParserService
    {
        Task<IResultWithError<ImageDataResponse>> SetImageForGroup(string imageBaseString, Guid groupId);
    }
}
