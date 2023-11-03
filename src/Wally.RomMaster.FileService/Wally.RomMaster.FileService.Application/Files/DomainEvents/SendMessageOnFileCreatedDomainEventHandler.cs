using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.RomMaster.FileService.Application.Messages.Files;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.DomainEvents;

public class SendMessageOnFileCreatedDomainEventHandler : IDomainEventHandler<FileCreatedDomainEvent>
{
	private readonly IBus _bus;
	private readonly IFileRepository _fileRepository;

	public SendMessageOnFileCreatedDomainEventHandler(IBus bus, IFileRepository fileRepository)
	{
		_bus = bus;
		_fileRepository = fileRepository;
	}

	public async Task HandleAsync(FileCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		var model = await _fileRepository.GetAsync(domainEvent.FileId, cancellationToken);
		var message = new FileCreatedMessage(model.Id.Value, model.Location.Location.LocalPath);

		await _bus.Publish(message, cancellationToken);
	}
}
