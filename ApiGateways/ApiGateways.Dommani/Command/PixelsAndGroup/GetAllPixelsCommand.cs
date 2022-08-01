using System.Collections.Generic;
using ApiGateways.Domain.Models.PixelsAndGroup;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class GetAllPixelsCommand : IRequest<IResultWithError<List<Pixel>>>
    {
    }
}
