using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.OData.Query;
using Wally.RomMaster.WolneLekturyService.Application.Abstractions;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Requests;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Responses;

namespace Wally.RomMaster.WolneLekturyService.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public sealed class GetUsersQuery : PagedQuery<GetUsersRequest, GetUsersResponse>
{
	public GetUsersQuery(ODataQueryOptions<GetUsersRequest> queryOptions)
		: base(queryOptions)
	{
	}
}
