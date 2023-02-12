using System;
using ApiGateways.Domain.Models.User.Response;
using Common.Errors;
using MediatR;

namespace ApiGateways.Domain.Command.User
{
	public class GetUserInfoCommand : IRequest<IResultWithError<GetUserInfoResponse>>
	{
		public Guid UserId { get; set; }
	}
}
