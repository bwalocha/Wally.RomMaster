using System;
using System.Collections.Concurrent;
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

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.FileService.BackgroundServices.Abstractions;
using Wally.RomMaster.FileService.BackgroundServices.Extensions;
using Wally.RomMaster.FileService.BackgroundServices.Models;

namespace Wally.RomMaster.FileService.BackgroundServices;

public class FileWatcherService : BackgroundService
{
	private readonly ConcurrentQueue<ICommand> _commandQueue = new();
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

		/*_watchers.AddRange(CreateWatchers(_settings.DatRoots, OnDatFileChanged));
		_watchers.AddRange(CreateWatchers(_settings.RomRoots, OnRomFileChanged));
		_watchers.AddRange(CreateWatchers(_settings.ToSortRoots, OnToSortFileChanged));*/

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
		var processCommandQueue = new Task(
			() => ProcessCommandQueueAsync(cancellationToken)
				.Wait(cancellationToken));
		processCommandQueue.Start();

		foreach (var watcher in _watchers)
		{
			watcher.EnableRaisingEvents = true;
		}

		return processCommandQueue.WaitAsync(cancellationToken);
	}

	private IEnumerable<FileSystemWatcher> CreateWatchers(
		List<FolderSettings> folders,
		FileSystemEventHandler onFileChanged)
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
				IncludeSubdirectories = folder.SearchOptions == SearchOption.AllDirectories,
				NotifyFilter =

					// NotifyFilters.LastAccess |
					NotifyFilters.LastWrite | NotifyFilters.FileName, // |
				// NotifyFilters.DirectoryName
			};

			if (onFileChanged != null)
			{
				watcher.Renamed += (sender, args) =>
				{
					OnChanged(onFileChanged, sender, args.ChangeType, args.FullPath, folder);
				};
				watcher.Created += (sender, args) => { OnCreated(onFileChanged, sender, args, folder); };
				watcher.Changed += (sender, args) =>
				{
					OnChanged(onFileChanged, sender, args.ChangeType, args.FullPath, folder);
				};
				watcher.Deleted += (sender, args) =>
				{
					OnChanged(onFileChanged, sender, args.ChangeType, args.FullPath, folder);
				};
			}

			watcher.Error += Watcher_Error;

			yield return watcher;
		}
	}

	private void OnCreated(
		FileSystemEventHandler onFileChanged,
		object sender,
		FileSystemEventArgs args,
		FolderSettings folder)
	{
		if (Directory.Exists(args.FullPath))
		{
			Action<string>? notify = null;
			notify = dir =>
			{
				foreach (var f in Directory.GetFiles(dir))
				{
					OnChanged(onFileChanged, sender, args.ChangeType, f, folder);
				}

				foreach (var d in Directory.GetDirectories(dir))
				{
					notify!(d);
				}
			};

			notify(args.FullPath);
		}
	}

	private void OnChanged(
		FileSystemEventHandler onChanged,
		object sender,
		WatcherChangeTypes changeType,
		string filePathName,
		FolderSettings folder)
	{
		// if directory: return or notify;
		// ...

		if (IsExcluded(filePathName, folder.Excludes))
		{
			_logger.LogDebug($"File '{filePathName}' excluded from watching.");
			return;
		}

		_logger.LogDebug($"File '{filePathName}' changed: '{changeType}'.");
		onChanged(
			sender,
			new FileSystemEventArgs(
				changeType,
				Path.GetDirectoryName(filePathName) ??
				throw new ArgumentException($"Wrong directory name for {filePathName}"),
				Path.GetFileName(filePathName)));
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

	private void OnDatFileChanged(object sender, FileSystemEventArgs e)
	{
		/*var command = new ScanFileCommand(SourceType.DatRoot, FileLocation.Create(new Uri(e.FullPath)));
		_commandQueue.Enqueue(command);*/
	}

	private void OnRomFileChanged(object sender, FileSystemEventArgs e)
	{
		/*var command = new ScanFileCommand(SourceType.Output, FileLocation.Create(new Uri(e.FullPath)));
		_commandQueue.Enqueue(command);*/
	}

	private void OnToSortFileChanged(object sender, FileSystemEventArgs e)
	{
		/*var command = new ScanFileCommand(SourceType.Input, FileLocation.Create(new Uri(e.FullPath)));
		_commandQueue.Enqueue(command);*/
	}

	private async Task ProcessCommandQueueAsync(CancellationToken cancellationToken)
	{
		do
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
		while (!cancellationToken.IsCancellationRequested);
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
}
