using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Wally.RomMaster.FileService.Application.Messages.Files;
using Wally.RomMaster.NotificationService.Application.Notifications.Commands;

namespace Wally.RomMaster.NotificationService.Infrastructure.Messaging.Consumers;

public class FileModifiedMessageConsumer : IConsumer<FileModifiedMessage>
{
	private readonly IMediator _mediator;
	
	public FileModifiedMessageConsumer(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	public async Task Consume(ConsumeContext<FileModifiedMessage> context)
	{
		var command = new BroadcastNotificationCommand("File modified", context.Message.Location);

		await _mediator.Send(command);
	}
}
