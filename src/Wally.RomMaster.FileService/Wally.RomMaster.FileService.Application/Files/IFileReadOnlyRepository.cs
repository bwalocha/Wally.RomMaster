using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Query;
using Wally.Lib.DDD.Abstractions.Requests;
using Wally.Lib.DDD.Abstractions.Responses;
using Wally.RomMaster.FileService.Domain.Abstractions;
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
