using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Wally.RomMaster.FileService.Application.Messages.Files;
using Wally.RomMaster.NotificationService.Application.Notifications.Commands;

namespace Wally.RomMaster.NotificationService.Infrastructure.Messaging.Consumers;

public class FileCreatedMessageConsumer : IConsumer<FileCreatedMessage>
{
	private readonly IMediator _mediator;
	
	public FileCreatedMessageConsumer(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	public async Task Consume(ConsumeContext<FileCreatedMessage> context)
	{
		var command = new BroadcastNotificationCommand("File created", context.Message.Location);

		await _mediator.Send(command);
	}
}
