using Wally.RomMaster.NotificationService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.NotificationService.Application.Contracts;

[ExcludeFromCodeCoverage]
public class PagedResponse<TResponse> : IResponse
{
	public PagedResponse(TResponse[] items, PageInfoResponse pageInfo)
	{
		Items = items;
		PageInfo = pageInfo;
	}

	public TResponse[] Items { get; }

	public PageInfoResponse PageInfo { get; }
}
