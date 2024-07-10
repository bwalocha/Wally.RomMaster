using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Application.Contracts;
using Wally.RomMaster.FileService.Application.Contracts.Files.Requests;
using Wally.RomMaster.FileService.Application.Contracts.Files.Responses;

namespace Wally.RomMaster.FileService.Application.Files.Queries;

public class GetFilesQueryHandler : QueryHandler<GetFilesQuery, PagedResponse<GetFilesResponse>>
{
	private readonly IFileReadOnlyRepository _fileRepository;

	public GetFilesQueryHandler(IFileReadOnlyRepository fileRepository)
	{
		_fileRepository = fileRepository;
	}

	public override Task<PagedResponse<GetFilesResponse>> HandleAsync(
		GetFilesQuery query,
		CancellationToken cancellationToken)
	{
		return _fileRepository.GetByPathIdAsync<GetFilesRequest, GetFilesResponse>(
			query.PathId,
			query.QueryOptions,
			cancellationToken);
	}
}
