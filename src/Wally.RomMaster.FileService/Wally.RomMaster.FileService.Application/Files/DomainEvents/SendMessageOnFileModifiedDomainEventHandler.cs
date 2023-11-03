using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.RomMaster.FileService.Application.Contracts.Responses.Files;
using Wally.RomMaster.FileService.Application.Messages.Files;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.DomainEvents;

public class SendMessageOnFileModifiedDomainEventHandler : IDomainEventHandler<FileModifiedDomainEvent>
{
	private readonly IBus _bus;
	private readonly IFileReadOnlyRepository _fileRepository;

	public SendMessageOnFileModifiedDomainEventHandler(IBus bus, /*ITopicProducer<FileModifiedMessage> producer,*/ IFileReadOnlyRepository fileRepository)
	{
		_bus = bus;
		_fileRepository = fileRepository;
	}

	public async Task HandleAsync(FileModifiedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		var model = await _fileRepository.GetAsync<GetFilesResponse>(domainEvent.FileId, cancellationToken);
		var message = new FileModifiedMessage(model.Id, model.Location.LocalPath);

		await _bus.Publish(message, cancellationToken);
	}
}
