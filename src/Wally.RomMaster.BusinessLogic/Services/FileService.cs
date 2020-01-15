using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wally.Database;
using Wally.RomMaster.Domain.Models;

namespace Wally.RomMaster.BusinessLogic.Services
{
    public abstract class FileService : BackgroundService
    {
        private readonly ILogger<FileService> logger;
        private readonly IOptions<AppSettings> appSettings;

        protected IUnitOfWorkFactory UnitOfWorkFactory { get; }

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

        private List<Exclude> excludes;
        protected List<Exclude> Excludes
        {
            get
            {
                if (excludes == null)
                {
                    excludes = GetFolders(appSettings)
                        .Where(a => a.Enabled)
                        .SelectMany(a => a.Excludes).ToList();
                }

                return this.excludes;
            }
        }

        public FileService(ILogger<FileService> logger, IOptions<AppSettings> appSettings, IUnitOfWorkFactory unitOfWorkFactory, HashAlgorithm crc32)
        {
            this.logger = logger;
            this.appSettings = appSettings;
            this.UnitOfWorkFactory = unitOfWorkFactory;
            this.crc32 = crc32;
        }

        public override void Dispose()
        {
            queue.Dispose();
            queueIsEmpty.Dispose();

            base.Dispose();
        }

        protected abstract IEnumerable<Folder> GetFolders(IOptions<AppSettings> appSettings);

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var folder in GetFolders(appSettings))
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

        private bool IsExcluded(string file) => IsExcluded(file, Excludes);

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

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("Starting...");
            cancellationToken.Register(() => logger.LogDebug("Background task is stopping."));

            while (!cancellationToken.IsCancellationRequested)
            {
                if (!queue.Any())
                {
                    queueIsEmpty.Set();
                }

                var item = await Task.Run(() => queue.Take(cancellationToken), cancellationToken).ConfigureAwait(false);
                logger.LogInformation($"Background task is procesing [{queue.Count}] item '{item}'.");
                var files = await Process(item).ConfigureAwait(false);
                foreach (var file in files)
                {
                    await PostProcessAsync(file, cancellationToken).ConfigureAwait(false);
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
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            List<File> files = new List<File>();
            File file = null;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repoFile = uow.GetReadRepository<File>();
                if (await repoFile.AnyAsync(a => a.Path == item.File).ConfigureAwait(false))
                {
                    logger.LogDebug($"File '{item.File}' already processed. Skipped.");
                    // Return already processed file
                    //
                    // files.Add(file);
                    return files;
                }

                if (IsFileLocked(item.File))
                {
                    logger.LogDebug($"File '{item.File}' access is denied. Skipped.");
                    return files;
                }

                // add file regardless it is archive
                string computedCrc32 = null;
                long size = 0;
                var writeRepoFile = uow.GetRepository<File>();

                // archive
                if (IsArchive(item.File))
                {
                    try
                    {
                        using System.IO.Stream stream = System.IO.File.OpenRead(item.File);
                        using var archive = SharpCompress.Archives.ArchiveFactory.Open(stream);
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
                                Crc = entry.Crc.ToString("X2", System.Globalization.CultureInfo.InvariantCulture),
                                Path = fileName,
                                Size = entry.Size
                            };

                            await writeRepoFile.AddAsync(file).ConfigureAwait(false);
                            files.Add(file);
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
                    using var stream = System.IO.File.Open(item.File, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                    size = stream.Length;
                    var hash = crc32.ComputeHash(stream);
                    computedCrc32 = BitConverter.ToString(hash).Replace("-", newValue: null, true, System.Globalization.CultureInfo.InvariantCulture);
                }

                // store file info
                file = new File
                {
                    Crc = computedCrc32, // null if file is archive package
                    Path = item.File,
                    Size = size // 0 if file is archive package
                };

                await writeRepoFile.AddAsync(file).ConfigureAwait(false);
                files.Add(file);

                await uow.CommitAsync().ConfigureAwait(false);
            }

            return files;
        }

        protected virtual Task PostProcessAsync(File file, CancellationToken cancellationToken)
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

                // stream = fileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None);
                stream = fileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None);
            }
            catch (System.IO.IOException)
            {
                // the file is unavailable because it is:
                // still being written to
                // or being processed by another thread
                // or does not exist
                return true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            // file is not locked
            return false;
        }

        protected static bool IsArchive(string file)
        {
            switch (System.IO.Path.GetExtension(file).ToUpperInvariant())
            {
                // Zip, GZip, BZip2, Tar, Rar, LZip, XZ'
                case ".RAR":
                case ".ZIP":
                    return true;
                case ".7Z":
                    return true;
                default:
                    return false;
            }
        }
    }
}
