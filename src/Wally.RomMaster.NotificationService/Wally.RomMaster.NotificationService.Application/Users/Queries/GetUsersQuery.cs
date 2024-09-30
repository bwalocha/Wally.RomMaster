using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.OData.Query;
using Wally.RomMaster.NotificationService.Application.Abstractions;
using Wally.RomMaster.NotificationService.Application.Contracts.Users.Requests;
using Wally.RomMaster.NotificationService.Application.Contracts.Users.Responses;

namespace Wally.RomMaster.NotificationService.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public sealed class GetUsersQuery : PagedQuery<GetUsersRequest, GetUsersResponse>
{
	public GetUsersQuery(ODataQueryOptions<GetUsersRequest> queryOptions)
		: base(queryOptions)
	{
	}
}
