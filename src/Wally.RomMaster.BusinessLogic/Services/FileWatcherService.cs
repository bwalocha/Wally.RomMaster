﻿namespace Wally.RomMaster.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    //using Common;
    using System.Linq;
    using Wally.RomMaster.Domain.Models;

    public class FileWatcherService : IHostedService, IDisposable
    {
        private readonly ILogger<FileWatcherService> logger;
        private readonly IOptions<AppSettings> appSettings;
        private readonly List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();

        public FileSystemEventHandler DatFileChanged { get; set; }
        public FileSystemEventHandler RomFileChanged { get; set; }
        public FileSystemEventHandler ToSortFileChanged { get; set; }

        public FileWatcherService(ILogger<FileWatcherService> logger, IOptions<AppSettings> appSettings)
        {
            this.logger = logger;
            this.appSettings = appSettings;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting...");

            watchers.AddRange(CreateWatchers(appSettings.Value.DatRoots, OnDatFileChanged));
            watchers.AddRange(CreateWatchers(appSettings.Value.RomRoots, OnRomFileChanged));
            watchers.AddRange(CreateWatchers(appSettings.Value.ToSortRoots, OnToSortFileChanged));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stopping...");

            watchers.ForEach((watcher) => watcher.EnableRaisingEvents = false);

            return Task.CompletedTask;
        }

        protected virtual void OnDatFileChanged(object sender, FileSystemEventArgs e)
        {
            DatFileChanged?.Invoke(sender, e);
        }

        protected virtual void OnRomFileChanged(object sender, FileSystemEventArgs e)
        {
            RomFileChanged?.Invoke(sender, e);
        }

        protected virtual void OnToSortFileChanged(object sender, FileSystemEventArgs e)
        {
            ToSortFileChanged?.Invoke(sender, e);
        }

        private IEnumerable<FileSystemWatcher> CreateWatchers(List<Folder> folders, FileSystemEventHandler onFileChanged = null)
        {
            foreach (var folder in folders)
            {
                if (!folder.Enabled)
                {
                    logger.LogWarning($"Folder '{folder.Path}' is not active. Skipping.");
                    continue;
                }

                if (!Directory.Exists(folder.Path))
                {
                    logger.LogWarning($"Folder '{folder.Path}' does not exist. Skipping.");
                    continue;
                }

                var watcher = new FileSystemWatcher(folder.Path, "*.*")
                {
                    IncludeSubdirectories = folder.SearchOptions == SearchOption.AllDirectories,
                    NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                       | NotifyFilters.FileName | NotifyFilters.DirectoryName
                };

                if (onFileChanged != null)
                {
                    watcher.Renamed += (sender, args) =>
                    {
                        OnChanged(onFileChanged, sender, args.ChangeType, args.FullPath, args.Name, folder);
                    };
                    watcher.Created += (sender, args) =>
                    {
                        if (Directory.Exists(args.FullPath))
                        {
                            Action<string> notify = null;
                            notify = (dir) =>
                            {
                                foreach (var f in Directory.GetFiles(dir))
                                {
                                    OnChanged(onFileChanged, sender, args.ChangeType, f, args.Name, folder);
                                }

                                foreach (var d in Directory.GetDirectories(dir))
                                {
                                    notify(d);
                                }
                            };

                            notify(args.FullPath);
                        }
                    };
                    watcher.Changed += (sender, args) =>
                    {
                        OnChanged(onFileChanged, sender, args.ChangeType, args.FullPath, args.Name, folder);
                    };
                    watcher.Deleted += (sender, args) =>
                    {
                        OnChanged(onFileChanged, sender, args.ChangeType, args.FullPath, args.Name, folder);
                    };
                }

                watcher.EnableRaisingEvents = folder.WatcherEnabled;

                yield return watcher;
            }
        }

        private void OnChanged(FileSystemEventHandler onChanged, object sender, WatcherChangeTypes changeType, string filePathName, string fileName, Folder folder)
        {
            // if directory: return or notify;
            // ...

            if (IsExcluded(filePathName, folder.Excludes))
            {
                logger.LogDebug($"File '{filePathName}' excluded from watching.");
            }
            else
            {
                logger.LogDebug($"File '{filePathName}' changed: '{changeType}'.");
                onChanged(sender, new FileSystemEventArgs(changeType, Path.GetDirectoryName(filePathName), Path.GetFileName(filePathName)));
            }
        }

        private bool IsExcluded(string file, List<Exclude> excludes)
        {
            if (!excludes.Any())
            {
                return false;
            }

            foreach (var exclude in excludes)
            {
                if (IsExcluded(file, exclude))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsExcluded(string file, Exclude exclude)
        {
            return exclude.Match(file);
        }

        public void Dispose()
        {
            foreach (var toDispose in watchers.ToArray())
            {
                toDispose?.Dispose();
            }
        }
    }
}