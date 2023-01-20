using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class UpdateHashCommandHandler : CommandHandler<UpdateHashCommand>
{
	private readonly IFileRepository _fileRepository;

	public UpdateHashCommandHandler(IFileRepository fileRepository)
	{
		_fileRepository = fileRepository;
	}

	public override async Task HandleAsync(UpdateHashCommand command, CancellationToken cancellationToken)
	{
		var model = await _fileRepository.GetAsync(command.FileId, cancellationToken);
		model.SetCrc32(command.Crc32);

		_fileRepository.Update(model);
	}
}
