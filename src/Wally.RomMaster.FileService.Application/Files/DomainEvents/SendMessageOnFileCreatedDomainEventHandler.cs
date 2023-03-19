using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.Lib.ServiceBus.Abstractions;
using Wally.RomMaster.FileService.Application.Messages.Files;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.DomainEvents;

public class SendMessageOnFileCreatedDomainEventHandler : IDomainEventHandler<FileCreatedDomainEvent>
{
	private readonly IFileRepository _fileRepository;
	private readonly IPublisher _publisher;

	public SendMessageOnFileCreatedDomainEventHandler(IPublisher publisher, IFileRepository fileRepository)
	{
		_publisher = publisher;
		_fileRepository = fileRepository;
	}

	public async Task HandleAsync(FileCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		var model = await _fileRepository.GetAsync(domainEvent.Id, cancellationToken);
		var message = new FileCreatedMessage(model.Id, model.Location.Location.LocalPath);
		await _publisher.PublishAsync(message, cancellationToken);
	}
}
