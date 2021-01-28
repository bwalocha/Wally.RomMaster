using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wally.RomMaster.Domain.Models;

namespace Wally.RomMaster.BusinessLogic.Services
{
	public class ClientService : BackgroundService
	{
		private readonly ILogger<ClientService> _logger;
		private readonly FileWatcherService _fileWatcherService;
		private readonly DatFileService _datFileService;
		private readonly RomFileService _romFileService;
		private readonly ToSortFileService _toSortFileService;
		private readonly FixService _fixService;

		public ClientService(
			ILogger<ClientService> logger,
			FileWatcherService fileWatcherService,
			DatFileService datFileService,
			RomFileService romFileService,
			ToSortFileService toSortFileService,
			FixService fixService)
		{
			this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this._fileWatcherService = fileWatcherService ?? throw new ArgumentNullException(nameof(fileWatcherService));
			this._datFileService = datFileService ?? throw new ArgumentNullException(nameof(datFileService));
			this._romFileService = romFileService ?? throw new ArgumentNullException(nameof(romFileService));
			this._toSortFileService = toSortFileService ?? throw new ArgumentNullException(nameof(toSortFileService));
			this._fixService = fixService ?? throw new ArgumentNullException(nameof(fixService));

			this._fileWatcherService.DatFileChanged += DatFileChanged;
			this._fileWatcherService.ToSortFileChanged += ToSortFileChanged;
		}

		private void DatFileChanged(object sender, FileSystemEventArgs e)
		{
			_datFileService.Enqueue(e.FullPath);

			// TODO: execute FixService for new Dat file data
			// this.fixService.Enqueue(e.FullPath);
		}

		private void ToSortFileChanged(object sender, FileSystemEventArgs e)
		{
			// TODO: wait for access to file
			_toSortFileService.Enqueue(e.FullPath);

			// TODO: wait for end of thr toSortService processing
			// TODO: execute FixService for new Dat file data
			// this.fixService.Enqueue(e.FullPath);
		}

		public override async Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug("Statring the application...");

			cancellationToken.Register(() => _logger.LogDebug("Background task is stopping..."));
			await _fileWatcherService.StartAsync(cancellationToken).ConfigureAwait(false);
			await _datFileService.StartAsync(cancellationToken).ConfigureAwait(false);
			await _datFileService.WaitForQueueEmptyAsync(cancellationToken).ConfigureAwait(false);

			await _romFileService.StartAsync(cancellationToken).ConfigureAwait(false);
			await _toSortFileService.StartAsync(cancellationToken).ConfigureAwait(false);

			_logger.LogDebug("Application has been started.");
			await base.StartAsync(cancellationToken).ConfigureAwait(false);
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug("Starting...");

			cancellationToken.Register(() => _logger.LogDebug("Background task is stopping..."));

			await _datFileService.WaitForQueueEmptyAsync(cancellationToken).ConfigureAwait(false);
			await _romFileService.WaitForQueueEmptyAsync(cancellationToken).ConfigureAwait(false);
			await _toSortFileService.WaitForQueueEmptyAsync(cancellationToken).ConfigureAwait(false);

			await _fixService.StartAsync(cancellationToken).ConfigureAwait(false);

			_logger.LogDebug("Background task is stopping.");
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug("Stopping the application...");

			// Run background task clean-up actions
			await _fileWatcherService.StopAsync(cancellationToken).ConfigureAwait(false);
			await _datFileService.StopAsync(cancellationToken).ConfigureAwait(false);
			await _romFileService.StopAsync(cancellationToken).ConfigureAwait(false);
			await _toSortFileService.StopAsync(cancellationToken).ConfigureAwait(false);
			await _fixService.StopAsync(cancellationToken).ConfigureAwait(false);

			_logger.LogDebug("Application has been stopped.");
		}
	}
}
