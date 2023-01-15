using MediatR;

using Microsoft.Extensions.Logging;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.Lib.ServiceBus.Abstractions;
using Wally.RomMaster.FileService.Messages.Files;
using Wally.RomMaster.HashService.Application.Hashes.Commands;

namespace Wally.RomMaster.HashService.Messaging.Consumers;

public class FileCreatedConsumer : Consumer<FileCreatedMessage>
{
	public FileCreatedConsumer(IMediator mediator, ILogger<FileCreatedConsumer> logger)
		: base(mediator, logger)
	{
	}

	protected override ICommand CreateCommand(FileCreatedMessage message)
	{
		return new ComputeHashCommand(message.Id, message.Location);
	}
}
