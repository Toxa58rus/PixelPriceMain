using System;
using ApiGateways.Domain.Models.Image.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.ImageParser
{
    public class ParseImageCommand : IRequest<IResultWithError<ImageDataResponse>>
    {
        public string ImageBaseString { get; set; }
        public int XCount { get; set; }
        public int YCount { get; set; }
        public Guid GroupId { get; set; }
    }
}
