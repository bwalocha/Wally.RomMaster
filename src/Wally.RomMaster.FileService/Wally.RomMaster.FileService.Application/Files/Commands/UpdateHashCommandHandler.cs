using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.FileService.Application.Abstractions;

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
		var model = await _fileRepository.GetOrDefaultAsync(command.FileId, cancellationToken);

		if (model is null)
		{
			return;
		}
		
		model.SetCrc32(command.Crc32)
			.SetMd5(command.Md5);

		_fileRepository.Update(model);
	}
}
