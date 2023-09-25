using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

using Wally.Lib.DDD.Abstractions.Responses;
using Wally.RomMaster.FileService.Application.Contracts.Requests.Files;
using Wally.RomMaster.FileService.Application.Contracts.Responses.Files;
using Wally.RomMaster.FileService.Application.Files.Queries;

namespace Wally.RomMaster.FileService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(int), 200, "application/json")]
public class FilesController : ControllerBase
{
	private readonly ISender _sender;

	public FilesController(ISender sender)
	{
		_sender = sender;
	}

	/// <summary>
	///     Gets File.
	/// </summary>
	/// <param name="pathId">Path Id.</param>
	/// <param name="queryOptions">OData query.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paths.</returns>
	/// <remarks>
	///     Sample request:
	///     GET /Paths/{id}/Files
	/// </remarks>
	[HttpGet("{pathId:guid}")]
	public async Task<ActionResult<PagedResponse<GetFilesResponse>>> GetAsync(
		Guid pathId,
		ODataQueryOptions<GetFilesRequest> queryOptions,
		CancellationToken cancellationToken)
	{
		var query = new GetFilesQuery(pathId, queryOptions);
		var response = await _sender.Send(query, cancellationToken);

		return Ok(response);
	}
}
