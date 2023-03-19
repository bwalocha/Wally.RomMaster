using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.OData.Query;

using Wally.Lib.DDD.Abstractions.Queries;
using Wally.RomMaster.FileService.Application.Contracts.Requests.Paths;
using Wally.RomMaster.FileService.Application.Contracts.Responses.Paths;

namespace Wally.RomMaster.FileService.Application.Paths.Queries;

[ExcludeFromCodeCoverage]
public class GetPathsQuery : PagedQuery<GetPathsRequest, GetPathsResponse>
{
	public GetPathsQuery(Guid? parentId, ODataQueryOptions<GetPathsRequest> queryOptions)
		: base(queryOptions)
	{
		ParentId = parentId;
	}

	public Guid? ParentId { get; }
}
