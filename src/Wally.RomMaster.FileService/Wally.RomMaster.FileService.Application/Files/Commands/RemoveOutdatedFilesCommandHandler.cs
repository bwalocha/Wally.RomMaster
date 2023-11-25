using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class RemoveOutdatedFilesCommandHandler : CommandHandler<RemoveOutdatedFilesCommand>
{
	private readonly IFileRepository _fileRepository;

	public RemoveOutdatedFilesCommandHandler(IFileRepository fileRepository)
	{
		_fileRepository = fileRepository;
	}

	public override Task HandleAsync(RemoveOutdatedFilesCommand command, CancellationToken cancellationToken)
	{
		Debugger.Break();

		// _fileRepository.RemoveOutdatedFiles(command.Timestamp);

		return Task.CompletedTask;
	}
}
