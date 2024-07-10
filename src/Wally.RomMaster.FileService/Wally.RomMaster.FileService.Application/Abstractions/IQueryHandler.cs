using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Wally.RomMaster.FileService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.FileService.Application.Abstractions;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
	where TQuery : IQuery<TResult>
	where TResult : IResponse
{
	Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
