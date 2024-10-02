using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Application.Abstractions;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
	where TQuery : IQuery<TResult>
	where TResult : IResponse
{
	Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
