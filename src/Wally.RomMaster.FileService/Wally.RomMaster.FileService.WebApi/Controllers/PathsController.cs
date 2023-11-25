using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Wally.Lib.DDD.Abstractions.Responses;
using Wally.RomMaster.FileService.Application.Contracts.Requests.Paths;
using Wally.RomMaster.FileService.Application.Contracts.Responses.Paths;
using Wally.RomMaster.FileService.Application.Paths.Queries;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(int), 200, "application/json")]
public partial class PathsController : ControllerBase
{
	private readonly ISender _sender;

	public PathsController(ISender sender)
	{
		_sender = sender;
	}

	/// <summary>
	///     Gets Root Paths.
	/// </summary>
	/// <param name="queryOptions">OData query.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paths.</returns>
	/// <remarks>
	///     Sample request:
	///     GET /Paths
	/// </remarks>
	[HttpGet]
	public async Task<ActionResult<PagedResponse<GetPathsResponse>>> GetRootAsync(
		ODataQueryOptions<GetPathsRequest> queryOptions,
		CancellationToken cancellationToken)
	{
		var query = new GetPathsQuery(null, queryOptions);
		var response = await _sender.Send(query, cancellationToken);

		return Ok(response);
	}

	/// <summary>
	///     Gets Children of the Path.
	/// </summary>
	/// <param name="id">Path Id.</param>
	/// <param name="queryOptions">OData query.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Paths.</returns>
	/// <remarks>
	///     Sample request:
	///     GET /Paths/{id}
	/// </remarks>
	[HttpGet("{id:guid}")]
	public async Task<ActionResult<PagedResponse<GetPathsResponse>>> GetAsync(
		Guid? id,
		ODataQueryOptions<GetPathsRequest> queryOptions,
		CancellationToken cancellationToken)
	{
		var query = new GetPathsQuery(id.HasValue ? new PathId(id.Value) : null, queryOptions);
		var response = await _sender.Send(query, cancellationToken);

		return Ok(response);
	}
}
