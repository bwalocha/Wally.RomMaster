using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.OData.Query;

using Wally.Lib.DDD.Abstractions.Queries;
using Wally.RomMaster.Contracts.Requests.Users;
using Wally.RomMaster.Contracts.Responses.Users;

namespace Wally.RomMaster.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public class GetUsersQuery : PagedQuery<GetUsersRequest, GetUsersResponse>
{
	public GetUsersQuery(ODataQueryOptions<GetUsersRequest> queryOptions)
		: base(queryOptions)
	{
	}
}
