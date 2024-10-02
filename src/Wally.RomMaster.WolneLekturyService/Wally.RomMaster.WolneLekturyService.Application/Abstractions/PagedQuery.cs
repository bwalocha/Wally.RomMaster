using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.OData.Query;
using Wally.RomMaster.WolneLekturyService.Application.Contracts;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Application.Abstractions;

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
