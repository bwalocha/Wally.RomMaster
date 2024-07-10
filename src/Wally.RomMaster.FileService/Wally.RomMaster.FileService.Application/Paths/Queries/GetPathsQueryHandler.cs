using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Application.Contracts;
using Wally.RomMaster.FileService.Application.Contracts.Paths.Requests;
using Wally.RomMaster.FileService.Application.Contracts.Paths.Responses;

namespace Wally.RomMaster.FileService.Application.Paths.Queries;

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
