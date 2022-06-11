using System;
using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Application.Files;

public class FileCreatedDomainEventHandler : IDomainEventHandler<FileCreatedDomainEvent>
{
	private readonly IFileRepository _fileRepository;

	public FileCreatedDomainEventHandler(IFileRepository fileRepository)
	{
		_fileRepository = fileRepository;
	}

	public async Task HandleAsync(FileCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		var model = await _fileRepository.GetAsync(domainEvent.Id, cancellationToken);

		if (model.IsArchivePackage())
		{
			throw new NotImplementedException("Archive packages not supported");
		}
	}
}
