using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Wally.RomMaster.FileService.Application.Messages.Files;

namespace Wally.RomMaster.NotificationService.Infrastructure.Messaging.Consumers;

public class FileModifiedMessageConsumer : IConsumer<FileModifiedMessage>
{
	private readonly IMediator _mediator;
	
	public FileModifiedMessageConsumer(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	public Task Consume(ConsumeContext<FileModifiedMessage> context)
	{
		throw new NotSupportedException();
	}
}
