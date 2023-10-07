using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.FileService.Application.Paths;
using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class ScanPathsCommandHandler : CommandHandler<ScanPathsCommand>
{
	private readonly IClockService _clockService;
	private readonly IPathRepository _pathRepository;

	public ScanPathsCommandHandler(
		IPathRepository pathRepository,
		IClockService clockService)
	{
		_pathRepository = pathRepository;
		_clockService = clockService;
	}

	public override async Task HandleAsync(ScanPathsCommand command, CancellationToken cancellationToken)
	{
		var createdPaths = new List<Path>();
		
		foreach (var location in command.Locations)
		{
			var path = await _pathRepository.GetOrDefaultAsync(location, cancellationToken);
			if (path == null)
			{
				var parentPath = await GetOrCreatePathAsync(location.Location.LocalPath, createdPaths, cancellationToken);
				path = Path.Create(parentPath, location.Location.LocalPath);
				
				createdPaths.Add(path);
				_pathRepository.Add(path);
			}
			/*else
			{
				path.Update(_clockService);
				_pathRepository.Update(path);
			}*/
		}
	}
	
	private async Task<Path?> GetOrCreatePathAsync(string pathName, List<Path> createdPaths, CancellationToken cancellationToken)
	{
		var name = System.IO.Path.GetDirectoryName(pathName);
		if (string.IsNullOrEmpty(name))
		{
			return null;
		}

		var path = createdPaths.FirstOrDefault(a => a.Name == pathName) ?? await _pathRepository.GetOrDefaultAsync(FileLocation.Create(new Uri(name)), cancellationToken);
		if (path == null)
		{
			var parent = await GetOrCreatePathAsync(name, createdPaths, cancellationToken);

			path = Path.Create(parent, name);
			createdPaths.Add(path);
		}

		return path;
	}
}
