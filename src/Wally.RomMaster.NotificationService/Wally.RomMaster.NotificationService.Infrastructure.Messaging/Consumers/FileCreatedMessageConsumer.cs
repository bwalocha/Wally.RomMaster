using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Wally.RomMaster.FileService.Application.Messages.Files;

namespace Wally.RomMaster.NotificationService.Infrastructure.Messaging.Consumers;

public class FileCreatedMessageConsumer : IConsumer<FileCreatedMessage>
{
	private readonly IMediator _mediator;
	
	public FileCreatedMessageConsumer(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	public Task Consume(ConsumeContext<FileCreatedMessage> context)
	{
		throw new NotSupportedException();
	}
}
