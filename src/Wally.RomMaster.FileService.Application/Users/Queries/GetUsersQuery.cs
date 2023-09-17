using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.OData.Query;

using Wally.RomMaster.FileService.Application.Contracts.Requests.Users;
using Wally.RomMaster.FileService.Application.Contracts.Responses.Users;
using Wally.Lib.DDD.Abstractions.Queries;

namespace Wally.RomMaster.FileService.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public sealed class GetUsersQuery : PagedQuery<GetUsersRequest, GetUsersResponse>
{
	public GetUsersQuery(ODataQueryOptions<GetUsersRequest> queryOptions)
		: base(queryOptions)
	{
	}
}
