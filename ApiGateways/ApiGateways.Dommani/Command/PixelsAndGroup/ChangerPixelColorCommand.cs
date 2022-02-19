using System;
using System.Collections.Generic;
using ApiGateways.Domain.Models.PixelsAndGroup.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class ChangerPixelColorCommand : IRequest<ResultWithError<List<ChangePixelColorResponse>>>
    {
        public List<Guid> PixelsId { get; set; }
        public int Color { get; set; }
        public Guid UserId { get; set; }
    }
}
