using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.OData.Query;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Application.Contracts.Files.Requests;
using Wally.RomMaster.FileService.Application.Contracts.Files.Responses;
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
