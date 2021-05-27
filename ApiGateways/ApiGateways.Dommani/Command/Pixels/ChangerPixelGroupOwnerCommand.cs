using Common.Models.Pixels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Dommain.Command.Pixels
{

    public class ChangerPixelGroupOwnerCommand : IRequest<List<PixelGroup>>
    {
        public List<PixelGroup> Groups { get; set; }
        public string UserId { get; set; }
    }
}
