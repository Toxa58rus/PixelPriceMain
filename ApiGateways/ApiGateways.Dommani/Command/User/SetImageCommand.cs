using System;
using ApiGateways.Domain.Models.User.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.User
{
    public class SetImageCommand : IRequest<IResultWithError>
    { 
	    public string Image { get; set; }
	    public Guid UserId { get; set; }
    }
}
