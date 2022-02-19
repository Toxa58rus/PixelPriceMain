using System;
using ApiGateways.Domain.Models.PixelsAndGroup;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.PixelsAndGroup
{
    public class CreatePixelGroupCommand : IRequest<ResultWithError<Guid>>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
