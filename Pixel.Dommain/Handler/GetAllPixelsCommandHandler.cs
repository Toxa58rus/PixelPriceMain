using Common.Models.Pixels;
using MediatR;
using Pixel.Dommain.Command;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pixel.Dommain.Handler
{
    public class GetAllPixelsCommandHandler : IRequestHandler<GetAllPixelsCommand, List<Pixels>>
    {
        public Task<List<Pixels>> Handle(GetAllPixelsCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
