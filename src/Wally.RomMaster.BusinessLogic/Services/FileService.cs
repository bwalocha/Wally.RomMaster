﻿namespace Wally.RomMaster.BusinessLogic.Services
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Models;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Security.Cryptography;
    using System;
    using System.Collections.Generic;
    using RomMaster.Domain.Models;
    using Wally.Database;

    public abstract class FileService : BackgroundService
    {
        private readonly ILogger<FileService> logger;
        private readonly IOptions<AppSettings> appSettings;
        protected readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly HashAlgorithm crc32;
        private readonly BlockingCollection<FileQueueItem> queue = new BlockingCollection<FileQueueItem>();
        private readonly ManualResetEvent queueIsEmpty = new ManualResetEvent(false);

        public ILogger<FileService> Logger
        {
            get
            {
                return logger;
            }
        }

        List<Exclude> excludes;
        protected List<Exclude> Excludes
        {
            get
            {
                if (excludes == null)
                {
                    excludes = GetFolders(this.appSettings)
                        .Where(a => a.Enabled)
                        .SelectMany(a => a.Excludes).ToList();
                }

                return excludes;
            }
        }

        public FileService(ILogger<FileService> logger, IOptions<AppSettings> appSettings, IUnitOfWorkFactory unitOfWorkFactory, HashAlgorithm crc32)
        {
            this.logger = logger;
            this.appSettings = appSettings;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.crc32 = crc32;
        }

        public override void Dispose()
        {
            this.queue.Dispose();
            this.queueIsEmpty.Dispose();

            base.Dispose();
        }


        protected abstract IEnumerable<Folder> GetFolders(IOptions<AppSettings> appSettings);

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var folder in GetFolders(this.appSettings))
            {
                if (!folder.Enabled)
                {
                    logger.LogWarning($"Folder '{folder.Path}' is not active. Skipping.");
                    continue;
                }

                logger.LogDebug($"Processing folder '{folder.Path}' ({folder.SearchOptions})");
                if (!System.IO.Directory.Exists(folder.Path))
                {
                    logger.LogWarning($"Folder '{folder.Path}' does not exist. Skipping.");
                    continue;
                }

                var files = System.IO.Directory.EnumerateFiles(folder.Path, "*.*", folder.SearchOptions);
                var filesCount = files.Count();
                var index = 0;
                foreach (var file in files)
                {
                    ++index;
                    logger.LogInformation($"Enqueuing [{(float)index / filesCount * 100,3:000}] file '{file}' ({index}/{filesCount})");
                    if (cancellationToken.IsCancellationRequested)
                    {
                        logger.LogWarning($"Processing file '{file}' ({index}/{filesCount}) has been cancelled.");
                        return;
                    }

                    Enqueue(file);
                }

                logger.LogDebug($"Finished processing folder '{folder.Path}'. Found {filesCount} files.");
            }

            await base.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        private bool IsExcluded(string file) => IsExcluded(file, this.Excludes);

        private static bool IsExcluded(string file, List<Exclude> excludes)
        {
            if (!excludes.Any())
            {
                return false;
            }

            foreach (var exclude in excludes)
            {
                if (FileService.IsExcluded(file, exclude))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsExcluded(string file, Exclude exclude) => exclude.Match(file);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogDebug("Starting...");
            stoppingToken.Register(() => logger.LogDebug("Background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                if (!queue.Any())
                {
                    queueIsEmpty.Set();
                }

                var item = await Task.Run(() => queue.Take(stoppingToken), stoppingToken).ConfigureAwait(false);
                logger.LogInformation($"Background task is procesing [{queue.Count}] item '{item}'.");
                var files = await Process(item).ConfigureAwait(false);
                foreach (var file in files)
                {
                    await PostProcess(file).ConfigureAwait(false);
                }
            }

            logger.LogDebug("Background task is stopping.");
        }

        public void Enqueue(string file)
        {
            if (IsExcluded(file))
            {
                logger.LogInformation($"File processing '{file}' excluded. Skipped.");
                return;
            }

            var item = new FileQueueItem
            {
                File = file
            };

            queue.Add(item);
            queueIsEmpty.Reset();
        }

        public Task WaitForQueueEmptyAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => queueIsEmpty.WaitOne(), cancellationToken);
        }

        protected virtual async Task<List<File>> Process(FileQueueItem item)
        {
            List<File> files = new List<File>();
            if (IsFileLocked(item.File))
            {
                logger.LogDebug($"File '{item.File}' access is denied. Skipped.");
                return files;
            }

            File file = null;
            using (var uow = unitOfWorkFactory.Create())
            {
                var repoFile = uow.GetRepository<File>();
                file = await repoFile.FindAsync(a => a.Path == item.File).ConfigureAwait(false);
                if (file != null)
                {
                    logger.LogDebug($"File '{item.File}' already processed. Skipped.");
                    files.Add(file);
                    return files;
                }

                // add file regardless it is archive
                string computedCrc32 = null;
                long size = 0;

                //archive
                if (IsArchive(item.File))
                {
                    try
                    {
                        using (System.IO.Stream stream = System.IO.File.OpenRead(item.File))
                        using (var archive = SharpCompress.Archives.ArchiveFactory.Open(stream))
                        {
                            foreach (var entry in archive.Entries.Where(a => !a.IsDirectory))
                            {
                                var fileName = $"{item.File}#{entry.Key}";

                                if (await repoFile.AnyAsync(a => a.Path == fileName).ConfigureAwait(false))
                                {
                                    continue;
                                }

                                // store file info
                                file = new File
                                {
                                    Crc = entry.Crc.ToString("X2"),
                                    Path = fileName,
                                    Size = entry.Size
                                };

                                await repoFile.AddAsync(file).ConfigureAwait(false);
                                files.Add(file);
                            }
                        }
                    }
                    catch (SharpCompress.Common.ArchiveException ex)
                    {
                        logger.LogError(ex, $"File '{item.File}' corrupted.");
                        return files;
                    }
                    catch (InvalidOperationException ex)
                    {
                        logger.LogError(ex, $"File '{item.File}' error.");
                        return files;
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        logger.LogError(ex, $"File '{item.File}' corrupted.");
                        return files;
                    }
                }
                else
                {
                    using (var stream = System.IO.File.Open(item.File, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
                    {
                        size = stream.Length;
                        //if (await repoFile.AnyAsync(a => a.Size == size).ConfigureAwait(false))
                        //{
                            var hash = crc32.ComputeHash(stream);
                            computedCrc32 = BitConverter.ToString(hash).Replace("-", "");
                        //}
                    }
                }

                // store file info
                file = new File
                {
                    Crc = computedCrc32, //null if file is archive package
                    Path = item.File,
                    Size = size //0 if file is archive package
                };

                await repoFile.AddAsync(file).ConfigureAwait(false);
                files.Add(file);

                await uow.CommitAsync().ConfigureAwait(false);
            }

            return files;
        }

        protected virtual Task PostProcess(File file)
        {
            return Task.CompletedTask;
        }

        private bool IsFileLocked(string file)
        {
            System.IO.FileStream stream = null;

            try
            {
                var fileInfo = new System.IO.FileInfo(file);
                if (!fileInfo.Exists)
                {
                    // file does not exist or is a directory
                    return true;
                }

                //stream = fileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None);
                stream = fileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None);
            }
            catch (System.IO.IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        protected bool IsArchive(string file)
        {
            switch (System.IO.Path.GetExtension(file).ToLower())
            {
                //Zip, GZip, BZip2, Tar, Rar, LZip, XZ'
                case ".rar":
                case ".zip":
                    return true;
                case ".7z":
                    return true;
                default:
                    return false;
            }
        }
    }
}