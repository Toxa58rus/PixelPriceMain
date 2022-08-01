using System;
using System.Collections.Generic;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Common.Errors;
using Contracts.PixelContract;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class GetPixelByGroupIdCommand : IRequest<IResultWithError<GetPixelByGroupIdResponse>>
    {
	    public Guid GroupId { get; set; }
    }
}
