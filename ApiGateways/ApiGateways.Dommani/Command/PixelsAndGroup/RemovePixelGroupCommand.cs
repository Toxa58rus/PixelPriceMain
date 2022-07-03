using System;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class RemovePixelGroupCommand : IRequest<IResultWithError>//IRequest<ResultWithError>
    {
	    public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }
}
