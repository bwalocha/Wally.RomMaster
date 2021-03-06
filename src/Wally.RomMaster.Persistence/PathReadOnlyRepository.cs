using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

using Wally.Lib.DDD.Abstractions.Requests;
using Wally.Lib.DDD.Abstractions.Responses;
using Wally.RomMaster.Application.Paths;
using Wally.RomMaster.Domain.Files;
using Wally.RomMaster.Persistence.Abstractions;

namespace Wally.RomMaster.Persistence;

public class PathReadOnlyRepository : ReadOnlyRepository<Path>, IPathReadOnlyRepository
{
	public PathReadOnlyRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}

	public Task<PagedResponse<TResponse>> GetByParentIdAsync<TRequest, TResponse>(
		Guid? parentId,
		ODataQueryOptions<TRequest> queryOptions,
		CancellationToken cancellationToken) where TRequest : class, IRequest where TResponse : class, IResponse
	{
		var query = GetReadOnlyEntitySet()
			.Where(a => a.ParentId == parentId);

		return GetAsync<TRequest, TResponse>(query, queryOptions, cancellationToken);
	}
}
