using System.IO;
using System.Security.Cryptography;
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
	private readonly HashAlgorithm _hashAlgorithm;

	public ScanFileCommandHandler(
		IFileRepository fileRepository,
		IClockService clockService,
		HashAlgorithm hashAlgorithm)
	{
		_fileRepository = fileRepository;
		_clockService = clockService;
		_hashAlgorithm = hashAlgorithm;
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

				return;
			}

			return;
		}

		if (file == null)
		{
			file = await File.CreateAsync(
				_clockService,
				fileInfo,
				command.SourceType,
				_hashAlgorithm,
				cancellationToken);

			_fileRepository.Add(file);
			return;
		}

		/*await file.UpdateAsync(_clockService, fileInfo, _hashAlgorithm, cancellationToken);

		_fileRepository.Update(file);*/
	}
}
