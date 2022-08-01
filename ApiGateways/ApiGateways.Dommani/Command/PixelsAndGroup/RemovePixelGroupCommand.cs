using System;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class RemovePixelGroupCommand : IRequest<IResultWithError>
    {
	    public Guid GroupId { get; set; }
    }
}
