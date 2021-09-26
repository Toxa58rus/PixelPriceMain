using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ApiGateways.Dommain.Handler
{
	public abstract class HandlerBase<TRequest, TResponse> :  IRequestHandler<TRequest, TResponse> 
		where TRequest : IRequest<TResponse>
	{

		protected abstract Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken); 
		
		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			return Execute(request, cancellationToken);
		}
	}
}
