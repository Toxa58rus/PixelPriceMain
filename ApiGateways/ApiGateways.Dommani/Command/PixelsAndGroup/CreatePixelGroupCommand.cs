using System;
using ApiGateways.Domain.Models.PixelsAndGroup;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class CreatePixelGroupCommand : IRequest<IResultWithError<Guid>>
    {
	    public string Name { get; set; }
    }
}
