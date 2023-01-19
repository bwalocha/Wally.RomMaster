using MediatR;

using Microsoft.Extensions.Logging;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.Lib.ServiceBus.Abstractions;
using Wally.RomMaster.FileService.Messages.Files;
using Wally.RomMaster.HashService.Application.Hashes.Commands;

namespace Wally.RomMaster.HashService.Messaging.Consumers;

public class FileCreatedMessageConsumer : Consumer<FileCreatedMessage>
{
	public FileCreatedMessageConsumer(IMediator mediator, ILogger<FileCreatedMessageConsumer> logger)
		: base(mediator, logger)
	{
	}

	protected override ICommand CreateCommand(FileCreatedMessage message)
	{
		return new ComputeHashCommand(message.Id, message.Location);
	}
}
