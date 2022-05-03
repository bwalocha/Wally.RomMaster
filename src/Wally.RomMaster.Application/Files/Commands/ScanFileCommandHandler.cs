using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.Domain.Abstractions;

using File = Wally.RomMaster.Domain.Files.File;

namespace Wally.RomMaster.Application.Files.Commands;

public class ScanFileCommandHandler : CommandHandler<ScanFileCommand>
{
	private readonly IClockService _clockService;
	private readonly IFileRepository _fileRepository;

	public ScanFileCommandHandler(IFileRepository fileRepository, IClockService clockService)
	{
		_fileRepository = fileRepository;
		_clockService = clockService;
	}

	public override async Task HandleAsync(ScanFileCommand command, CancellationToken cancellationToken)
	{
		var file = await _fileRepository.GetOrDefaultAsync(command.Location, cancellationToken);
		var fileInfo = new FileInfo(command.Location.Location.LocalPath);

		if (file == null)
		{
			file = File.Create(_clockService, fileInfo);

			_fileRepository.Add(file);
			return;
		}

		file.Update(_clockService, fileInfo);

		_fileRepository.Update(file);
	}
}
