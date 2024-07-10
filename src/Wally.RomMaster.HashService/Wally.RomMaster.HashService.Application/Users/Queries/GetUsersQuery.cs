using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.OData.Query;
using Wally.RomMaster.HashService.Application.Abstractions;
using Wally.RomMaster.HashService.Application.Contracts.Users.Requests;
using Wally.RomMaster.HashService.Application.Contracts.Users.Responses;

namespace Wally.RomMaster.HashService.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public sealed class GetUsersQuery : PagedQuery<GetUsersRequest, GetUsersResponse>
{
	public GetUsersQuery(ODataQueryOptions<GetUsersRequest> queryOptions)
		: base(queryOptions)
	{
	}
}
