using System;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class GetGroupByUserIdCommand : IRequest<IResultWithError<GetGroupResponse>>// IRequest<ResultWithError<GetGroupResponse>>
    {
	    public Guid UserId { get; set; }
    }
}
