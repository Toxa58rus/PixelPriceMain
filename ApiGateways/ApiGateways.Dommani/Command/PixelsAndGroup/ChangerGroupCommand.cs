using System;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class ChangerGroupCommand : IRequest<IResultWithError<ChangeGroupResponse>>
    {
	    public Guid GroupId { get; set; }
		public string Name { get; set; }
		public string Massage { get; set; }
    }
}
