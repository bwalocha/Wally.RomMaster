using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Wally.Lib.DDD.Abstractions.Responses;
using Wally.RomMaster.FileService.Application.Contracts.Requests.Files;
using Wally.RomMaster.FileService.Application.Contracts.Responses.Files;
using Wally.RomMaster.FileService.Application.Files.Queries;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.WebApi.Controllers;

public partial class PathsController
{
	/// <summary>
	///     Gets Files of the Path.
	/// </summary>
	/// <param name="pathId">Path Id.</param>
	/// <param name="queryOptions">OData query.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paths.</returns>
	/// <remarks>
	///     Sample request:
	///     GET /Paths/{id}/Files
	/// </remarks>
	[HttpGet("/Paths/{pathId:guid}/Files")]
	public async Task<ActionResult<PagedResponse<GetFilesResponse>>> GetAsync(
		Guid pathId,
		ODataQueryOptions<GetFilesRequest> queryOptions,
		CancellationToken cancellationToken)
	{
		var query = new GetFilesQuery(new PathId(pathId), queryOptions);
		var response = await _sender.Send(query, cancellationToken);

		return Ok(response);
	}
}
