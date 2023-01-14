using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.OData.Query;

using Wally.RomMaster.HashService.Contracts.Requests.Users;
using Wally.RomMaster.HashService.Contracts.Responses.Users;
using Wally.Lib.DDD.Abstractions.Queries;

namespace Wally.RomMaster.HashService.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public class GetUsersQuery : PagedQuery<GetUsersRequest, GetUsersResponse>
{
	public GetUsersQuery(ODataQueryOptions<GetUsersRequest> queryOptions)
		: base(queryOptions)
	{
	}
}
