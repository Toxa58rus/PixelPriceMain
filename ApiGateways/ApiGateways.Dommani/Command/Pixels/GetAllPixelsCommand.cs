using MediatR;
using System.Collections.Generic;
using PixelsData = Common.Models.Pixels.Pixel;

namespace ApiGateways.Dommain.Command.Pixels
{
    public class GetAllPixelsCommand : IRequest<List<PixelsData>>
    {
    }
}
