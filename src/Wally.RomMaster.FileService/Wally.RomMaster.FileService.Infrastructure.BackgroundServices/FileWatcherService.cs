﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.FileService.Application.Files.Commands;
using Wally.RomMaster.FileService.Domain.Files;
using Wally.RomMaster.FileService.Infrastructure.BackgroundServices.Abstractions;
using Wally.RomMaster.FileService.Infrastructure.BackgroundServices.Extensions;
using Wally.RomMaster.FileService.Infrastructure.BackgroundServices.Models;
using Path = System.IO.Path;

namespace Wally.RomMaster.FileService.Infrastructure.BackgroundServices;

public class FileWatcherService : BackgroundService
{
	public delegate Task FileSystemEventHandlerAsync(
		object sender,
		FileSystemEventArgs e,
		CancellationToken cancellationToken);

	private readonly ILogger<FileWatcherService> _logger;
	private readonly IServiceProvider _serviceProvider;
	private readonly ISettings _settings;
	private readonly List<FileSystemWatcher> _watchers = new();

	public FileWatcherService(ILogger<FileWatcherService> logger, ISettings settings, IServiceProvider serviceProvider)
	{
		_logger = logger;
		_settings = settings;
		_serviceProvider = serviceProvider;
	}

	public override Task StartAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("Starting...");

		_watchers.AddRange(CreateWatchers(_settings.FolderSettings, OnFileChangedAsync, cancellationToken));

		return base.StartAsync(cancellationToken);
	}

	public override Task StopAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("Stopping...");

		_watchers.ForEach(watcher => watcher.EnableRaisingEvents = false);

		return base.StopAsync(cancellationToken);
	}

	protected override Task ExecuteAsync(CancellationToken cancellationToken)
	{
		foreach (var watcher in _watchers)
		{
			watcher.EnableRaisingEvents = true;
		}

		return Task.CompletedTask;
	}

	private IEnumerable<FileSystemWatcher> CreateWatchers(
		List<FolderSettings> folders,
		FileSystemEventHandlerAsync onFileChanged,
		CancellationToken cancellationToken)
	{
		foreach (var folder in folders)
		{
			if (!folder.Enabled)
			{
				_logger.LogWarning($"Folder '{folder.Path}' is not active. Skipping.");
				continue;
			}

			if (!folder.WatcherEnabled)
			{
				_logger.LogWarning($"Watcher for '{folder.Path}' is not active. Skipping.");
				continue;
			}

			if (!Directory.Exists(folder.Path.LocalPath))
			{
				_logger.LogWarning($"Folder '{folder.Path}' does not exist. Skipping.");
				continue;
			}

			var watcher = new FileSystemWatcher(folder.Path.LocalPath, "*.*")
			{
				InternalBufferSize = 8192 * 8,
				IncludeSubdirectories = folder.SearchOptions == SearchOption.AllDirectories,
				NotifyFilter =

					// NotifyFilters.LastAccess |
					NotifyFilters.LastWrite | NotifyFilters.FileName, // |
				// NotifyFilters.DirectoryName
			};

			if (onFileChanged != null)
			{
				watcher.Renamed += async (sender, args) =>
				{
					// TODO: remove and add?
					await OnChangedAsync(
						onFileChanged,
						sender,
						args.ChangeType,
						args.FullPath,
						folder,
						cancellationToken);
				};
				watcher.Created += async (sender, args) =>
				{
					await OnCreatedAsync(onFileChanged, sender, args, folder, cancellationToken);
				};
				watcher.Changed += async (sender, args) =>
				{
					await OnChangedAsync(
						onFileChanged,
						sender,
						args.ChangeType,
						args.FullPath,
						folder,
						cancellationToken);
				};
				watcher.Deleted += async (sender, args) =>
				{
					await OnChangedAsync(
						onFileChanged,
						sender,
						args.ChangeType,
						args.FullPath,
						folder,
						cancellationToken);
				};
			}

			watcher.Error += Watcher_Error;

			yield return watcher;
		}
	}

	private async Task OnCreatedAsync(
		FileSystemEventHandlerAsync onFileChanged,
		object sender,
		FileSystemEventArgs args,
		FolderSettings folder,
		CancellationToken cancellationToken)
	{
		if (Directory.Exists(args.FullPath))
		{
			Func<string, Task>? notify = null;
			notify = async dir =>
			{
				foreach (var f in Directory.GetFiles(dir))
				{
					await OnChangedAsync(onFileChanged, sender, args.ChangeType, f, folder, cancellationToken);
				}

				foreach (var d in Directory.GetDirectories(dir))
				{
					await notify!(d);
				}
			};

			await notify(args.FullPath);
		}
		else
		{
			await OnChangedAsync(onFileChanged, sender, args.ChangeType, args.FullPath, folder, cancellationToken);
		}
	}

	private async Task OnChangedAsync(
		FileSystemEventHandlerAsync onChanged,
		object sender,
		WatcherChangeTypes changeType,
		string filePathName,
		FolderSettings folder,
		CancellationToken cancellationToken)
	{
		if (Directory.Exists(filePathName))
		{
			// is a directory
			await ReScanPathAsync(filePathName, folder, cancellationToken);
			return;
		}

		if (IsExcluded(filePathName, folder.Excludes))
		{
			_logger.LogDebug($"File '{filePathName}' excluded from watching.");
			return;
		}

		_logger.LogDebug($"File '{filePathName}' changed: '{changeType}'.");
		await onChanged(
			sender,
			new FileSystemEventArgs(
				changeType,
				Path.GetDirectoryName(filePathName) ??
				throw new ArgumentException($"Wrong directory name for {filePathName}"),
				Path.GetFileName(filePathName)),
			cancellationToken);
	}

	private bool IsExcluded(string file, List<ExcludeSettings> excludes)
	{
		return excludes.Any(a => IsExcluded(file, a));
	}

	private static bool IsExcluded(string file, ExcludeSettings exclude)
	{
		return exclude.Match(file);
	}

	private void Watcher_Error(object sender, ErrorEventArgs e)
	{
		_logger.LogError("Watch file error: {0}", e.GetException());

		Debugger.Break();
	}

	private async Task OnFileChangedAsync(object sender, FileSystemEventArgs e, CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();
		var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
		var command = new ScanFileCommand(new FileLocation(new Uri(e.FullPath)));
		try
		{
			await mediator.Send(command, cancellationToken);
		}
		catch (Exception exception)
		{
			_logger.LogError("OnFileChangedAsync error: {0}", exception);
		}
	}

	public override void Dispose()
	{
		foreach (var watcher in _watchers.ToArray())
		{
			watcher.EnableRaisingEvents = false;
			watcher.Dispose();
		}

		base.Dispose();
	}

	private async Task ReScanPathAsync(string folderPath, FolderSettings folder, CancellationToken cancellationToken)
	{
		foreach (var path in Directory.EnumerateDirectories(folderPath, "*.*", folder.SearchOptions))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				_logger.LogDebug("ScanAsync cancelled.");
				return;
			}

			if (IsExcluded(path, folder.Excludes))
			{
				_logger.LogDebug($"Path '{path}' excluded from scanning.");
				continue;
			}

			_logger.LogDebug($"Path '{path}' found.");

			var command = new ScanPathCommand(new FileLocation(new Uri(path)));
			using var scope = _serviceProvider.CreateScope();
			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
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
}
