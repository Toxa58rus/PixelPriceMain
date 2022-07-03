using System;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class GetGroupByIdCommand : IRequest<IResultWithError<GetGroupResponse>>// IRequest<ResultWithError<GetGroupResponse>>
    {
	    public Guid GroupId { get; set; }
    }
}
