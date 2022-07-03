using System;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class ChangerGroupCommand : IRequest<IResultWithError<ChangeGroupResponse>> //IRequest<ResultWithError<ChangeGroupResponse>>
    {
	    public Guid GroupId { get; set; }
		public string Name { get; set; }
	    public Guid UserId { get; set; }
	    public string Massage { get; set; }
    }
}
