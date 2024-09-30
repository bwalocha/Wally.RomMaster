using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.OData.Query;
using Wally.RomMaster.NotificationService.Application.Contracts;
using Wally.RomMaster.NotificationService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.NotificationService.Application.Abstractions;

[ExcludeFromCodeCoverage]
public class PagedQuery<TRequest, TResponse> : IQuery<PagedResponse<TResponse>>
	where TRequest : IRequest
	where TResponse : IResponse
{
	protected PagedQuery(ODataQueryOptions<TRequest> queryOptions)
	{
		QueryOptions = queryOptions;
	}

	public ODataQueryOptions<TRequest> QueryOptions { get; }
}
