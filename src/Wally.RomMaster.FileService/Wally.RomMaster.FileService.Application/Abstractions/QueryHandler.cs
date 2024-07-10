using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Wally.RomMaster.FileService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.FileService.Application.Abstractions;

public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
	where TQuery : IQuery<TResult>, IRequest<TResult>
	where TResult : IResponse
{
	public abstract Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);

	public Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
	{
		return HandleAsync(request, cancellationToken);
	}
}
