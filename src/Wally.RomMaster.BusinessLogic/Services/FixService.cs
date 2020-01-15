using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wally.Database;
using Wally.RomMaster.Domain.Models;

namespace Wally.RomMaster.BusinessLogic.Services
{
    public class FixService : BackgroundService
    {
        private readonly ILogger<FixService> logger;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly BlockingCollection<FileQueueItem> queue = new BlockingCollection<FileQueueItem>();

        public FixService(ILogger<FixService> logger, IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.logger = logger;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogDebug("Starting...");
            stoppingToken.Register(() => logger.LogDebug("Background task is stopping."));

            logger.LogInformation("Background task is procesing.");
            await Process(stoppingToken).ConfigureAwait(false);

            while (!stoppingToken.IsCancellationRequested)
            {
                var item = await Task.Run(() => queue.Take(stoppingToken), stoppingToken).ConfigureAwait(false);
                // logger.LogInformation($"Background task is procesing [{queue.Count}] item '{item}'.");
                await Process(item).ConfigureAwait(false);
            }

            logger.LogDebug("Background task is stopping.");
        }

        public void Enqueue(File file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var item = new FileQueueItem
            {
                File = file.Path
            };

            queue.Add(item);
        }

        private Task Process(CancellationToken stoppingToken)
        {
            logger.LogInformation("Finding fixes...");

            using (var uow = this.unitOfWorkFactory.Create())
            {
                var repoFile = uow.GetRepository<File>();
                var files = repoFile.SqlQuery($@"
                    SELECT f.*
                    FROM File f
                    JOIN Rom r ON f.crc = r.crc AND f.size = r.size
                    WHERE f.size <> 0 
                    ORDER BY f.path");

                var foundCount = files.Count();
                logger.LogInformation($"Found '{foundCount}' files to fix (including already fixed).");

                foreach (var file in files)
                {
                    stoppingToken.ThrowIfCancellationRequested();

                    Enqueue(file);
                }
            }

            return Task.CompletedTask;
        }

        private Task Process(FileQueueItem item)
        {
            var proggress = queue.Count;

            if (!System.IO.File.Exists(item.File))
            {
                // IsArchive?
                // ...
                // logger.LogWarning($"[{proggress}] File '{item.File}' does not exist. Skipping.");
                return Task.CompletedTask;
            }

            logger.LogInformation($"[{proggress}] Finding fix for '{item.File}'...");

            return Task.CompletedTask;
        }
    }
}
