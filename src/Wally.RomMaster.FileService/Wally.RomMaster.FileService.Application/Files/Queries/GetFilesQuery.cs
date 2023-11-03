using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.OData.Query;

using Wally.Lib.DDD.Abstractions.Queries;
using Wally.RomMaster.FileService.Application.Contracts.Requests.Files;
using Wally.RomMaster.FileService.Application.Contracts.Responses.Files;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.Queries;

[ExcludeFromCodeCoverage]
public sealed class GetFilesQuery : PagedQuery<GetFilesRequest, GetFilesResponse>
{
	public GetFilesQuery(PathId pathId, ODataQueryOptions<GetFilesRequest> queryOptions)
		: base(queryOptions)
	{
		PathId = pathId;
	}

	public PathId PathId { get; }
}
