using System.Threading.Tasks;

using MassTransit;

using MediatR;

using Wally.RomMaster.FileService.Application.Files.Commands;
using Wally.RomMaster.FileService.Domain.Files;
using Wally.RomMaster.HashService.Application.Messages.Hashes;

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
		var command = new UpdateHashCommand(new FileId(message.FileId), message.Crc32);

		return _mediator.Send(command);
	}
}
