using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.RomMaster.Domain.Abstractions;
using Wally.RomMaster.Domain.Files;

using File = Wally.RomMaster.Domain.Files.File;

namespace Wally.RomMaster.Application.Files;

public class FileCreatedDomainEventHandler : IDomainEventHandler<FileCreatedDomainEvent>
{
	private readonly IClockService _clockService;
	private readonly IFileRepository _fileRepository;

	public FileCreatedDomainEventHandler(IFileRepository fileRepository, IClockService clockService)
	{
		_fileRepository = fileRepository;
		_clockService = clockService;
	}

	public async Task HandleAsync(FileCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		var model = await _fileRepository.GetAsync(domainEvent.Id, cancellationToken);

		if (model.IsArchivePackage())
		{
			await using (var zipToOpen = new FileStream(model.Location.Location.LocalPath, FileMode.Open, FileAccess.Read))
			{
				using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read);
				foreach (var entry in archive.Entries)
				{
					var file = File.Create(_clockService, model, entry);

					_fileRepository.Add(file);
				}
			}
		}
	}
}
