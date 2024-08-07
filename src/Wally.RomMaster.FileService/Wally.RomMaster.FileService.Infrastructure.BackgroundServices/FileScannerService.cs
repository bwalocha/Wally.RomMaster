﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
			_logger.LogWarning("Folder '{FolderPath}' is not active - skipping", folder.Path);
			return;
		}

		if (!Directory.Exists(folder.Path.LocalPath))
		{
			_logger.LogWarning("Folder '{FolderPath}' does not exist - skipping", folder.Path);
			return;
		}

		using var scope = _serviceProvider.CreateScope();
		var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
		foreach (var paths in GetDeepestDirectoryInfos(folder, folder.Path.LocalPath, cancellationToken)
					.Chunk(100))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug("ScanAsync cancelled");
				return;
			}

			var command = new ScanPathsCommand(paths.Select(a => new FileLocation(new Uri(a)))
				.ToArray());
			try
			{
				await mediator.Send(command, cancellationToken);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, "Cannot handle the command");
			}
		}

		foreach (var files in Directory.EnumerateFiles(folder.Path.LocalPath, "*.*", folder.SearchOptions)
					.Chunk(100))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug("ScanAsync cancelled");
				return;
			}

			/*if (IsExcluded(file, folder.Excludes))
			{
				_logger.LogDebug($"File '{file}' excluded from scanning.");
				continue;
			}

			_logger.LogDebug($"File '{file}' found.");

			var command = new ScanFileCommand(FileLocation.Create(new Uri(file)));*/

			var command = new ScanFilesCommand(
				files
					.Where(a => !IsExcluded(a, folder.Excludes))
					.Select(a => new FileLocation(new Uri(a)))
					.ToArray());

			try
			{
				await mediator.Send(command, cancellationToken);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, "Cannot handle the command");
			}
		}
	}

	private bool IsExcluded(string file, List<ExcludeSettings> excludes)
	{
		return excludes.Exists(a => IsExcluded(file, a));
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
			.Order()
			.ToList();

		yield return directoryInfo;

		if (!children.Any() || folder.SearchOptions == SearchOption.TopDirectoryOnly)
		{
			yield break;
		}

		foreach (var path in children)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug("ScanAsync cancelled");
				yield break;
			}

			foreach (var child in GetDeepestDirectoryInfos(folder, path, cancellationToken))
			{
				yield return child;
			}
		}
	}
}
