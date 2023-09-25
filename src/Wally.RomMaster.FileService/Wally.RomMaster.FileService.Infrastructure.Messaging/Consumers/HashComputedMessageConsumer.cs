using System.Threading.Tasks;

using MassTransit;

using MediatR;

using Wally.RomMaster.FileService.Application.Files.Commands;
using Wally.RomMaster.HashService.Messages.Hashes;

namespace Wally.RomMaster.FileService.Infrastructure.Messaging.Consumers;

public class HashComputedMessageConsumer : IConsumer<HashComputedMessage>
{
	private readonly IMediator _mediator;

	public HashComputedMessageConsumer(IMediator mediator)
	{
		_mediator = mediator;
	}

	public Task Consume(ConsumeContext<HashComputedMessage> context)
	{
		var message = context.Message;
		var command = new UpdateHashCommand(message.FileId, message.Crc32);

		return _mediator.Send(command);
	}
}
