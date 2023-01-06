using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.OData.Query;

using Wally.Lib.DDD.Abstractions.Requests;
using Wally.Lib.DDD.Abstractions.Responses;
using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Paths;

public interface IPathReadOnlyRepository : IReadOnlyRepository<Path>
{
	Task<PagedResponse<TResponse>> GetByParentIdAsync
		<TRequest, TResponse>(
			Guid? parentId,
			ODataQueryOptions<TRequest> queryOptions,
			CancellationToken cancellationToken) where TRequest : class, IRequest where TResponse : class, IResponse;
}
