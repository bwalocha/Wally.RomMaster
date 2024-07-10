using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Query;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Application.Contracts;
using Wally.RomMaster.FileService.Application.Contracts.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files;

public interface IFileReadOnlyRepository : IReadOnlyRepository<File, FileId>
{
	Task<PagedResponse<TResponse>> GetByPathIdAsync
		<TRequest, TResponse>(
			PathId pathId,
			ODataQueryOptions<TRequest> queryOptions,
			CancellationToken cancellationToken)
		where TRequest : class, IRequest
		where TResponse : class, IResponse;
}
