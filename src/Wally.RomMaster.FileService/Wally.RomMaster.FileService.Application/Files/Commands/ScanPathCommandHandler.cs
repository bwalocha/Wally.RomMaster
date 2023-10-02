using System;
using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.FileService.Application.Paths;
using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class ScanPathCommandHandler : CommandHandler<ScanPathCommand>
{
	private readonly IClockService _clockService;
	private readonly IPathRepository _pathRepository;

	public ScanPathCommandHandler(
		IPathRepository pathRepository,
		IClockService clockService)
	{
		_pathRepository = pathRepository;
		_clockService = clockService;
	}

	public override async Task HandleAsync(ScanPathCommand command, CancellationToken cancellationToken)
	{
		var path = await _pathRepository.GetOrDefaultAsync(command.Location, cancellationToken);
		if (path == null)
		{
			var parentPath = await GetOrCreatePathAsync(command.Location.Location.LocalPath, cancellationToken);
			path = Path.Create(parentPath, command.Location.Location.LocalPath);
	
			_pathRepository.Add(path);
		}
		else
		{
			path.Update(_clockService);
			_pathRepository.Update(path);
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
