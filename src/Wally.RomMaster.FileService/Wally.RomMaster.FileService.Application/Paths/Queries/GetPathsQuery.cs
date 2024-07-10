using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.OData.Query;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Application.Contracts.Paths.Requests;
using Wally.RomMaster.FileService.Application.Contracts.Paths.Responses;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Paths.Queries;

[ExcludeFromCodeCoverage]
public sealed class GetPathsQuery : PagedQuery<GetPathsRequest, GetPathsResponse>
{
	public GetPathsQuery(PathId? parentId, ODataQueryOptions<GetPathsRequest> queryOptions)
		: base(queryOptions)
	{
		ParentId = parentId;
	}

	public PathId? ParentId { get; }
}
