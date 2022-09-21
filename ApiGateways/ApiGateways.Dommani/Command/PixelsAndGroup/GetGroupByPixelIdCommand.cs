using System;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class GetGroupByPixelIdCommand : IRequest<IResultWithError<GetGroupResponse>>
    {
	    public Guid PixelId { get; set; }
    }
}
