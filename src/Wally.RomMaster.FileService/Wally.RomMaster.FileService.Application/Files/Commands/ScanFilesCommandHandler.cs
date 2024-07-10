using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Application.Messages.Files;
using Wally.RomMaster.FileService.Application.Paths;
using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;
using File = Wally.RomMaster.FileService.Domain.Files.File;
using Path = Wally.RomMaster.FileService.Domain.Files.Path;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class ScanFilesCommandHandler : CommandHandler<ScanFilesCommand>
{
	private readonly IBus _bus;
	private readonly IClockService _clockService;

	private readonly IFileRepository _fileRepository;

	private readonly IPathRepository _pathRepository;

	public ScanFilesCommandHandler(
		IFileRepository fileRepository,
		IPathRepository pathRepository,
		IClockService clockService,
		IBus bus)
	{
		_fileRepository = fileRepository;
		_pathRepository = pathRepository;
		_clockService = clockService;
		_bus = bus;
	}

	public override async Task HandleAsync(ScanFilesCommand command, CancellationToken cancellationToken)
	{
		foreach (var location in command.Locations)
		{
			var file = await _fileRepository.GetOrDefaultAsync(location, cancellationToken);
			var fileInfo = new FileInfo(location.Value.LocalPath);

			if (!fileInfo.Exists)
			{
				if (file != null)
				{
					_fileRepository.Remove(file);

					// TODO: file does not exist. Remove entry?
					// If the file is a Zip Archive then remove also inner files
					// ...
					if (file.IsArchivePackage())
					{
						Debugger.Break();
					}

					return;
				}

				return;
			}

			if (file == null)
			{
				var path = await GetOrCreatePathAsync(location.Value.LocalPath, cancellationToken);

				file = File.Create(
					_clockService,
					path,
					fileInfo);

				_fileRepository.Add(file);
			}
			else if (file.HasChanged(fileInfo))
			{
				file.Update(_clockService, fileInfo);

				_fileRepository.Update(file);
			}
			else if (file.Crc32 is null || file.Md5 is null)
			{
				var message = new FileModifiedMessage(file.Id.Value, file.Location.Value.LocalPath);

				await _bus.Publish(message, cancellationToken);
			}
		}
	}

	private async Task<Path?> GetOrCreatePathAsync(string pathName, CancellationToken cancellationToken)
	{
		var name = System.IO.Path.GetDirectoryName(pathName);
		if (string.IsNullOrEmpty(name))
		{
			return null;
		}

		var path = await _pathRepository.GetOrDefaultAsync(new FileLocation(new Uri(name)), cancellationToken);
		if (path == null)
		{
			var parent = await GetOrCreatePathAsync(name, cancellationToken);

			path = Path.Create(parent, name);
		}

		return path;
	}
}
