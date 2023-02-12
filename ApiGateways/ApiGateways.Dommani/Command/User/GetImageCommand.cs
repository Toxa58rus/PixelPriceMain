using System;
using ApiGateways.Domain.Models.User.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.User
{
	public class GetImageCommand : IRequest<IResultWithError<string>>
	{
		public Guid UserId { get; set; }
	}
}
