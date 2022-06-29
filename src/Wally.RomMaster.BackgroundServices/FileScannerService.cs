using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.Application.Files.Commands;
using Wally.RomMaster.BackgroundServices.Abstractions;
using Wally.RomMaster.BackgroundServices.Extensions;
using Wally.RomMaster.BackgroundServices.Models;
using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.BackgroundServices;

public class FileScannerService : BackgroundService
{
	private readonly IClockService _clockService;
	private readonly ConcurrentQueue<ICommand> _commandQueue = new();
	private readonly ILogger<FileScannerService> _logger;
	private readonly IServiceProvider _serviceProvider;
	private readonly ISettings _settings;

	public FileScannerService(
		ILogger<FileScannerService> logger,
		ISettings settings,
		IClockService clockService,
		IServiceProvider serviceProvider)
	{
		_logger = logger;
		_settings = settings;
		_clockService = clockService;
		_serviceProvider = serviceProvider;
	}

	public override Task StartAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("Starting...");

		return base.StartAsync(cancellationToken);
	}

	public override Task StopAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("Stopping...");

		return base.StopAsync(cancellationToken);
	}

	protected override async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		var manualResetEvent = new ManualResetEvent(false);
		var processCommandQueue = new Task(
			() => ProcessCommandQueueAsync(manualResetEvent, cancellationToken)
				.Wait(cancellationToken));

		processCommandQueue.Start();

		await Task.WhenAll(
			ScanAsync(SourceType.DatRoot, _settings.DatRoots, cancellationToken),
			ScanAsync(SourceType.Output, _settings.RomRoots, cancellationToken),
			ScanAsync(SourceType.Input, _settings.ToSortRoots, cancellationToken));

		manualResetEvent.Set();
		await processCommandQueue.WaitAsync(cancellationToken);

		var command = new RemoveOutdatedFilesCommand(_clockService.StartTimestamp);
		using var scope = _serviceProvider.CreateScope();
		var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
		await mediator.Send(command);
	}

	private async Task ScanAsync(SourceType sourceType, List<FolderSettings> folders, CancellationToken cancellationToken)
	{
		foreach (var folder in folders)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug("ScanAsync cancelled.");
				return;
			}

			if (!folder.Enabled)
			{
				_logger.LogWarning($"Folder '{folder.Path}' is not active. Skipping.");
				continue;
			}

			if (!Directory.Exists(folder.Path.LocalPath))
			{
				_logger.LogWarning($"Folder '{folder.Path}' does not exist. Skipping.");
				continue;
			}

			await ScanAsync(sourceType, folder, cancellationToken);
		}
	}

	private async Task ScanAsync(SourceType sourceType, FolderSettings folder, CancellationToken cancellationToken)
	{
		await ScanAsync(sourceType, folder.Path.LocalPath, folder.Excludes, cancellationToken);

		foreach (var directory in Directory.EnumerateDirectories(folder.Path.LocalPath, "*.*", folder.SearchOptions))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug("ScanAsync cancelled.");
				return;
			}

			await ScanAsync(sourceType, directory, folder.Excludes, cancellationToken);
		}
	}

	private async Task ScanAsync(
		SourceType sourceType,
		string directory,
		List<ExcludeSettings> excludes,
		CancellationToken cancellationToken)
	{
		foreach (var file in Directory.EnumerateFiles(directory, "*.*", SearchOption.TopDirectoryOnly))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug("ScanAsync cancelled.");
				return;
			}

			if (IsExcluded(file, excludes))
			{
				_logger.LogDebug($"File '{file}' excluded from scanning.");
				return;
			}

			_logger.LogDebug($"File '{file}' found.");

			var command = new ScanFileCommand(sourceType, FileLocation.Create(new Uri(file)));

			while (_commandQueue.Count >= 1000)
			{
				await Task.Delay(1000, cancellationToken);
			}

			_commandQueue.Enqueue(command);
		}
	}

	private bool IsExcluded(string file, List<ExcludeSettings> excludes)
	{
		return excludes.Any(a => IsExcluded(file, a));
	}

	private static bool IsExcluded(string file, ExcludeSettings exclude)
	{
		return exclude.Match(file);
	}

	private async Task ProcessCommandQueueAsync(ManualResetEvent manualResetEvent, CancellationToken cancellationToken)
	{
		do
		{
			await ProcessCommandQueueAsync(cancellationToken);
		} 
		while (!manualResetEvent.WaitOne(1000));

		await ProcessCommandQueueAsync(cancellationToken);
	}

	private async Task ProcessCommandQueueAsync(CancellationToken cancellationToken)
	{
		while (_commandQueue.TryDequeue(out var command))
		{
			using var scope = _serviceProvider.CreateScope();
			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			try
			{
				await mediator.Send(command, cancellationToken);
			}
			catch (Exception exception)
			{
				_logger.LogError("ProcessCommandQueueAsync error: {0}", exception);
			}
		}

		await Task.Delay(1000);
	}
}
