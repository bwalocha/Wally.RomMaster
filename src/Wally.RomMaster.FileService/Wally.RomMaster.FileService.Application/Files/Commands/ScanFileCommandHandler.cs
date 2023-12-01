using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.FileService.Application.Paths;
using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;
using File = Wally.RomMaster.FileService.Domain.Files.File;
using Path = Wally.RomMaster.FileService.Domain.Files.Path;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class ScanFileCommandHandler : CommandHandler<ScanFileCommand>
{
	private readonly IClockService _clockService;

	private readonly IFileRepository _fileRepository;

	private readonly IPathRepository _pathRepository;

	public ScanFileCommandHandler(
		IFileRepository fileRepository,
		IPathRepository pathRepository,
		IClockService clockService)
	{
		_fileRepository = fileRepository;
		_pathRepository = pathRepository;
		_clockService = clockService;
	}

	public override async Task HandleAsync(ScanFileCommand command, CancellationToken cancellationToken)
	{
		var file = await _fileRepository.GetOrDefaultAsync(command.Location, cancellationToken);
		var fileInfo = new FileInfo(command.Location.Location.LocalPath);

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
			var path = await GetOrCreatePathAsync(command.Location.Location.LocalPath, cancellationToken);

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
	}

	private async Task<Path?> GetOrCreatePathAsync(string pathName, CancellationToken cancellationToken)
	{
		var name = System.IO.Path.GetDirectoryName(pathName);
		if (string.IsNullOrEmpty(name))
		{
			return null;
		}

		var path = await _pathRepository.GetOrDefaultAsync(FileLocation.Create(new Uri(name)), cancellationToken);
		if (path == null)
		{
			var parent = await GetOrCreatePathAsync(name, cancellationToken);

			path = Path.Create(parent, name);
		}

		return path;
	}
}
