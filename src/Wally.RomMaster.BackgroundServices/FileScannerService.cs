using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Wally.RomMaster.Application.Files.Commands;
using Wally.RomMaster.BackgroundServices.Abstractions;
using Wally.RomMaster.BackgroundServices.Extensions;
using Wally.RomMaster.BackgroundServices.Models;

namespace Wally.RomMaster.BackgroundServices;

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

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		Scan(_settings.DatRoots);
		Scan(_settings.RomRoots);
		Scan(_settings.ToSortRoots);

		var command = new RemoveOutdatedFilesCommand(_clockService.StartTimestamp);
		using var scope = _serviceProvider.CreateScope();
		var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
		await mediator.Send(command);
	}

	private void Scan(List<FolderSettings> folders)
	{
		foreach (var folder in folders)
		{
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

			Scan(folder);
		}
	}

	private void Scan(FolderSettings folder)
	{
		Scan(folder.Path.LocalPath, folder.Excludes);

		foreach (var directory in Directory.EnumerateDirectories(folder.Path.LocalPath, "*.*", folder.SearchOptions))
		{
			Scan(directory, folder.Excludes);
		}
	}

	private void Scan(string directory, List<ExcludeSettings> excludes)
	{
		foreach (var file in Directory.EnumerateFiles(directory, "*.*", SearchOption.TopDirectoryOnly))
		{
			if (IsExcluded(file, excludes))
			{
				_logger.LogDebug($"File '{file}' excluded from scanning.");
				return;
			}

			_logger.LogDebug($"File '{file}' found.");

			// ...
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
}
