using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Query;
using Wally.RomMaster.HashService.Application.Contracts;
using Wally.RomMaster.HashService.Application.Contracts.Abstractions;
using Wally.RomMaster.HashService.Domain.Abstractions;

namespace Wally.RomMaster.HashService.Application.Abstractions;

public interface IReadOnlyRepository<TEntity, TStronglyTypedId>
	where TEntity : Entity<TEntity, TStronglyTypedId>
	where TStronglyTypedId : new()
{
	Task<bool> ExistsAsync(TStronglyTypedId id, CancellationToken cancellationToken);

	Task<TResponse> GetAsync<TResponse>(TStronglyTypedId id, CancellationToken cancellationToken)
		where TResponse : IResponse;

	Task<PagedResponse<TResponse>> GetAsync<TRequest, TResponse>(ODataQueryOptions<TRequest> queryOptions,
		CancellationToken cancellationToken)
		where TRequest : class, IRequest
		where TResponse : class, IResponse;
}
