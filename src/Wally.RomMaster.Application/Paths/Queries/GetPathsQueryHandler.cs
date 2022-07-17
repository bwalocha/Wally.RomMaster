using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.Queries;
using Wally.Lib.DDD.Abstractions.Responses;
using Wally.RomMaster.Contracts.Requests.Paths;
using Wally.RomMaster.Contracts.Responses.Paths;

namespace Wally.RomMaster.Application.Paths.Queries;

public class GetPathsQueryHandler : QueryHandler<GetPathsQuery, PagedResponse<GetPathsResponse>>
{
	private readonly IPathReadOnlyRepository _pathRepository;

	public GetPathsQueryHandler(IPathReadOnlyRepository pathRepository)
	{
		_pathRepository = pathRepository;
	}

	public override Task<PagedResponse<GetPathsResponse>> HandleAsync(
		GetPathsQuery query,
		CancellationToken cancellationToken)
	{
		return _pathRepository.GetByParentIdAsync<GetPathsRequest, GetPathsResponse>(
			query.ParentId,
			query.QueryOptions,
			cancellationToken);
	}
}
