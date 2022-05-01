﻿using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.Application.Files.Commands;

public class RemoveOutdatedFilesCommandHandler : CommandHandler<RemoveOutdatedFilesCommand>
{
	private readonly IFileRepository _fileRepository;

	public RemoveOutdatedFilesCommandHandler(IFileRepository fileRepository)
	{
		_fileRepository = fileRepository;
	}

	public override Task HandleAsync(RemoveOutdatedFilesCommand command, CancellationToken cancellationToken)
	{
		_fileRepository.RemoveOutdatedFiles(command.Timestamp);

		return Task.CompletedTask;
	}
}
