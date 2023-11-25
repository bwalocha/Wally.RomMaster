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
using Wally.RomMaster.FileService.Application.Files.Commands;
using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;
using Wally.RomMaster.FileService.Infrastructure.BackgroundServices.Abstractions;
using Wally.RomMaster.FileService.Infrastructure.BackgroundServices.Extensions;
using Wally.RomMaster.FileService.Infrastructure.BackgroundServices.Models;

namespace Wally.RomMaster.FileService.Infrastructure.BackgroundServices;

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
		var scanners = _settings.FolderSettings.Select(a => ScanAsync(a, cancellationToken));

		await Task.WhenAll(scanners);

		var command = new RemoveOutdatedFilesCommand(_clockService.StartTimestamp);
		using var scope = _serviceProvider.CreateScope();
		var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
		await mediator.Send(command, cancellationToken);
	}

	private async Task ScanAsync(FolderSettings folder, CancellationToken cancellationToken)
	{
		if (!folder.Enabled)
		{
			_logger.LogWarning($"Folder '{folder.Path}' is not active. Skipping.");
			return;
		}

		if (!Directory.Exists(folder.Path.LocalPath))
		{
			_logger.LogWarning($"Folder '{folder.Path}' does not exist. Skipping.");
			return;
		}

		using var scope = _serviceProvider.CreateScope();
		var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
		foreach (var paths in GetPartitions(GetDeepestDirectoryInfos(folder, folder.Path.LocalPath, cancellationToken)))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug("ScanAsync cancelled.");
				return;
			}

			var command = new ScanPathsCommand(paths.Select(a => FileLocation.Create(new Uri(a)))
				.ToArray());
			try
			{
				await mediator.Send(command, cancellationToken);
			}
			catch (Exception exception)
			{
				_logger.LogError("Error: '{0}'", exception);
			}
		}

		foreach (var file in Directory.EnumerateFiles(folder.Path.LocalPath, "*.*", folder.SearchOptions))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug("ScanAsync cancelled.");
				return;
			}

			if (IsExcluded(file, folder.Excludes))
			{
				_logger.LogDebug($"File '{file}' excluded from scanning.");
				continue;
			}

			_logger.LogDebug($"File '{file}' found.");

			var command = new ScanFileCommand(FileLocation.Create(new Uri(file)));
			/*
			using var scope = _serviceProvider.CreateScope();
			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			*/
			try
			{
				await mediator.Send(command, cancellationToken);
			}
			catch (Exception exception)
			{
				_logger.LogError("Error: '{0}'", exception);
			}
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

	private IEnumerable<string> GetDeepestDirectoryInfos(FolderSettings folder, string directoryInfo,
		CancellationToken cancellationToken)
	{
		_logger.LogDebug("Path '{DirectoryInfo}' scanning...", directoryInfo);

		var children = Directory
			.EnumerateDirectories(directoryInfo, "*.*", SearchOption.TopDirectoryOnly)
			.Where(a => !IsExcluded(a, folder.Excludes))
			.ToList();

		if (!children.Any() || folder.SearchOptions == SearchOption.TopDirectoryOnly)
		{
			yield return directoryInfo;
		}

		foreach (var path in children)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug("ScanAsync cancelled.");
				yield break;
			}

			foreach (var child in GetDeepestDirectoryInfos(folder, path, cancellationToken))
			{
				yield return child;
			}
		}
	}

	private IEnumerable<IEnumerable<T>> GetPartitions<T>(IEnumerable<T> items, int partitionSize = 100)
	{
		var partitionIndex = 0;
		var partition = items.ToList();
		do
		{
			partition = partition.Skip(partitionSize * partitionIndex)
				.ToList();
			if (!partition.Any())
			{
				yield break;
			}

			yield return partition.Take(partitionSize);

			partitionIndex++;
		}
		while (true);
	}
}
