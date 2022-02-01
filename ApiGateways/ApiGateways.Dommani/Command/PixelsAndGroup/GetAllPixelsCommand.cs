using System.Collections.Generic;
using ApiGateways.Domain.Models.PixelsAndGroup;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class GetAllPixelsCommand : IRequest<List<Pixel>>
    {
    }
}
