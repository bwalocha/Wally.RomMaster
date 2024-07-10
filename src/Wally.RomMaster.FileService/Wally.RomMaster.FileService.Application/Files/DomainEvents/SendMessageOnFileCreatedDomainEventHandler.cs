using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Application.Messages.Files;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.DomainEvents;

public class SendMessageOnFileCreatedDomainEventHandler : IDomainEventHandler<FileCreatedDomainEvent>
{
	private readonly IBus _bus;
	private readonly IFileRepository _fileRepository;
	private readonly ILogger<SendMessageOnFileCreatedDomainEventHandler> _logger;

	public SendMessageOnFileCreatedDomainEventHandler(IBus bus, IFileRepository fileRepository,
		ILogger<SendMessageOnFileCreatedDomainEventHandler> logger)
	{
		_bus = bus;
		_fileRepository = fileRepository;
		_logger = logger;
	}

	public async Task HandleAsync(FileCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		var model = await _fileRepository.GetAsync(domainEvent.FileId, cancellationToken);
		var message = new FileCreatedMessage(model.Id.Value, model.Location.Value.LocalPath);

		_logger.LogInformation($"Publishing: {message.GetType().Name}");

		await _bus.Publish(message, cancellationToken);
	}
}
