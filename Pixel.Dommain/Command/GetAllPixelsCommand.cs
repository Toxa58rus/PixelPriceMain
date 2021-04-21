using Common.Models.Pixels;
using MediatR;
using System.Collections.Generic;

namespace Pixel.Dommain.Command
{
    public class GetAllPixelsCommand : IRequest<List<Pixels>>
    {
    }
}
