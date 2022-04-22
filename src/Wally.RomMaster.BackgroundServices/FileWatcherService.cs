using System;
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

using Wally.RomMaster.Application.Users.Commands;
using Wally.RomMaster.BackgroundServices.Abstractions;
using Wally.RomMaster.BackgroundServices.Extensions;
using Wally.RomMaster.BackgroundServices.Models;

namespace Wally.RomMaster.BackgroundServices;

public class FileWatcherService : BackgroundService
{
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

		_watchers.AddRange(CreateWatchers(_settings.DatRoots, OnDatFileChangedAsync, cancellationToken));
		_watchers.AddRange(CreateWatchers(_settings.RomRoots, OnRomFileChangedAsync, cancellationToken));
		_watchers.AddRange(CreateWatchers(_settings.ToSortRoots, OnToSortFileChangedAsync, cancellationToken));

		return base.StartAsync(cancellationToken);
	}

	public override Task StopAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("Stopping...");

		_watchers.ForEach(watcher => watcher.EnableRaisingEvents = false);

		return base.StopAsync(cancellationToken);
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		// throw new NotImplementedException();
		return Task.CompletedTask;
	}

	private IEnumerable<FileSystemWatcher> CreateWatchers(
		List<FolderSettings> folders,
		FileSystemEventHandlerAsync onFileChangedAsync,
		CancellationToken cancellationToken)
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

			var watcher = new FileSystemWatcher(folder.Path, "*.*")
			{
				IncludeSubdirectories = folder.SearchOptions == SearchOption.AllDirectories,
				NotifyFilter =

					// NotifyFilters.LastAccess |
					NotifyFilters.LastWrite | NotifyFilters.FileName, // |
				// NotifyFilters.DirectoryName
			};

			if (onFileChangedAsync != null)
			{
				watcher.Renamed += async (sender, args) =>
				{
					await OnChangedAsync(
						onFileChangedAsync,
						sender,
						args.ChangeType,
						args.FullPath,
						folder,
						cancellationToken);
				};
				watcher.Created += async (sender, args) =>
				{
					await OnCreatedAsync(onFileChangedAsync, sender, args, folder, cancellationToken);
				};
				watcher.Changed += async (sender, args) =>
				{
					await OnChangedAsync(
						onFileChangedAsync,
						sender,
						args.ChangeType,
						args.FullPath,
						folder,
						cancellationToken);
				};
				watcher.Deleted += async (sender, args) =>
				{
					await OnChangedAsync(
						onFileChangedAsync,
						sender,
						args.ChangeType,
						args.FullPath,
						folder,
						cancellationToken);
				};
			}

			watcher.Error += Watcher_Error;
			watcher.EnableRaisingEvents = folder.WatcherEnabled;

			yield return watcher;
		}
	}

	private async Task OnCreatedAsync(
		FileSystemEventHandlerAsync onFileChangedAsync,
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
					await OnChangedAsync(onFileChangedAsync, sender, args.ChangeType, f, folder, cancellationToken);
				}

				foreach (var d in Directory.GetDirectories(dir))
				{
					await notify!(d);
				}
			};

			await notify(args.FullPath);
		}
	}

	private Task OnChangedAsync(
		FileSystemEventHandlerAsync onChangedAsync,
		object sender,
		WatcherChangeTypes changeType,
		string filePathName,
		FolderSettings folder,
		CancellationToken cancellationToken)
	{
		// if directory: return or notify;
		// ...

		if (IsExcluded(filePathName, folder.Excludes))
		{
			_logger.LogDebug($"File '{filePathName}' excluded from watching.");
			return Task.CompletedTask;
		}

		_logger.LogDebug($"File '{filePathName}' changed: '{changeType}'.");
		return onChangedAsync(
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

	private Task OnDatFileChangedAsync(object sender, FileSystemEventArgs e, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}

	private Task OnRomFileChangedAsync(object sender, FileSystemEventArgs e, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}

	private async Task OnToSortFileChangedAsync(
		object sender,
		FileSystemEventArgs e,
		CancellationToken cancellationToken)
	{
		// https://stackoverflow.com/questions/876473/is-there-a-way-to-check-if-a-file-is-in-use
		// https://programmer.ink/think/is-there-a-way-to-check-if-the-file-is-in-use.html
		var command = new UpdateUserCommand(Guid.NewGuid(), e.FullPath);
		using var scope = _serviceProvider.CreateScope();
		var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
		try
		{
			await mediator.Send(command, cancellationToken);
		}
		catch (Exception exception)
		{
			_logger.LogError("OnToSortFileChangedAsync error: {0}", exception);
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

	private delegate Task FileSystemEventHandlerAsync(
		object sender,
		FileSystemEventArgs args,
		CancellationToken cancellationToken);
}
