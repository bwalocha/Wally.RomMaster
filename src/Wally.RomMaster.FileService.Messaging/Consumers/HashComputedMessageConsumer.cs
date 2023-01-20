using MediatR;

using Microsoft.Extensions.Logging;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.Lib.ServiceBus.Abstractions;
using Wally.RomMaster.FileService.Application.Files.Commands;
using Wally.RomMaster.HashService.Messages.Hashes;

namespace Wally.RomMaster.FileService.Messaging.Consumers;

public class HashComputedMessageConsumer : Consumer<HashComputedMessage>
{
	public HashComputedMessageConsumer(IMediator mediator, ILogger<HashComputedMessageConsumer> logger)
		: base(mediator, logger)
	{
	}

	protected override ICommand CreateCommand(HashComputedMessage message)
	{
		return new UpdateHashCommand(message.FileId, message.Crc32);
	}
}
