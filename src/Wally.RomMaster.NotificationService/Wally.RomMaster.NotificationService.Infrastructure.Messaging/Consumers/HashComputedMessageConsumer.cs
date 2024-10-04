using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Wally.RomMaster.HashService.Application.Messages.Hashes;
using Wally.RomMaster.NotificationService.Application.Notifications.Commands;

namespace Wally.RomMaster.NotificationService.Infrastructure.Messaging.Consumers;

public class HashComputedMessageConsumer : IConsumer<HashComputedMessage>
{
	private readonly IMediator _mediator;

	public HashComputedMessageConsumer(IMediator mediator)
	{
		_mediator = mediator;
	}

	public async Task Consume(ConsumeContext<HashComputedMessage> context)
	{
		var command = new BroadcastNotificationCommand("Hash computed",
			$"File ID: {context.Message.FileId}, CRC32: {context.Message.Crc32}, MD5: {context.Message.Md5}");

		await _mediator.Send(command);
	}
}
