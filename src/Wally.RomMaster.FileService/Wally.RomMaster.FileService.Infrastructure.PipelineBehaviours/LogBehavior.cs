﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Wally.RomMaster.FileService.Infrastructure.PipelineBehaviours;

public class LogBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	private readonly ILogger<LogBehavior<TRequest, TResponse>> _logger;

	public LogBehavior(ILogger<LogBehavior<TRequest, TResponse>> logger)
	{
		_logger = logger;
	}

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		var correlationId = Guid.NewGuid();

		// https://stackoverflow.com/questions/56600156/simple-serialize-odataqueryoptions
		_logger.LogInformation(
			$"[{correlationId}] Executing request handler for request type: '{typeof(TRequest).Name}' and response type: '{typeof(TResponse).Name}'.");
		var stopWatch = Stopwatch.StartNew();
		try
		{
			return await next();
		}
		finally
		{
			stopWatch.Stop();

			if (stopWatch.ElapsedMilliseconds > 2000)
			{
				_logger.LogWarning($"[{correlationId}] Executed in {stopWatch.ElapsedMilliseconds} ms.");
			}
			else
			{
				_logger.LogInformation($"[{correlationId}] Executed in {stopWatch.ElapsedMilliseconds} ms.");
			}
		}
	}
}
