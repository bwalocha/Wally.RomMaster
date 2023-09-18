using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

using Wally.Lib.DDD.Abstractions.Requests;
using Wally.Lib.DDD.Abstractions.Responses;
using Wally.RomMaster.FileService.Application.Files;
using Wally.RomMaster.FileService.Domain.Files;
using Wally.RomMaster.FileService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.FileService.Infrastructure.Persistence;

public class FileReadOnlyRepository : ReadOnlyRepository<File>, IFileReadOnlyRepository
{
	public FileReadOnlyRepository(DbContext context, IMapper mapper)
		: base(context, mapper)
	{
	}

	public Task<PagedResponse<TResponse>> GetByPathIdAsync<TRequest, TResponse>(
		Guid pathId,
		ODataQueryOptions<TRequest> queryOptions,
		CancellationToken cancellationToken) where TRequest : class, IRequest where TResponse : class, IResponse
	{
		var query = GetReadOnlyEntitySet()
			.Where(a => a.PathId == pathId);

		return GetAsync<TRequest, TResponse>(query, queryOptions, cancellationToken);
	}
}
