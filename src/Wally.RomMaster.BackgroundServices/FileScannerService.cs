using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Wally.RomMaster.BackgroundServices.Abstractions;
using Wally.RomMaster.BackgroundServices.Extensions;
using Wally.RomMaster.BackgroundServices.Models;

namespace Wally.RomMaster.BackgroundServices;

public class FileScannerService : BackgroundService
{
	private readonly ILogger<FileScannerService> _logger;
	private readonly ISettings _settings;

	public FileScannerService(ILogger<FileScannerService> logger, ISettings settings)
	{
		_logger = logger;
		_settings = settings;
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

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		Scan(_settings.DatRoots);
		Scan(_settings.RomRoots);
		Scan(_settings.ToSortRoots);

		return Task.CompletedTask;
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

			if (!Directory.Exists(folder.Path))
			{
				_logger.LogWarning($"Folder '{folder.Path}' does not exist. Skipping.");
				continue;
			}

			Scan(folder);
		}
	}

	private void Scan(FolderSettings folder)
	{
		Scan(folder.Path, folder.Excludes);

		foreach (var directory in Directory.EnumerateDirectories(folder.Path, "*.*", folder.SearchOptions))
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
