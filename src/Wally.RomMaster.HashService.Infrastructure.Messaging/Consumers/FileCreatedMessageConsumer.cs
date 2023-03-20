using System.Threading.Tasks;

using MassTransit;

using MediatR;

using Wally.RomMaster.FileService.Messages.Files;
using Wally.RomMaster.HashService.Application.Hashes.Commands;

namespace Wally.RomMaster.HashService.Infrastructure.Messaging.Consumers;

public class FileCreatedMessageConsumer : IConsumer<FileCreatedMessage>
{
	private readonly IMediator _mediator;

	public FileCreatedMessageConsumer(IMediator mediator)
	{
		_mediator = mediator;
	}

	public Task Consume(ConsumeContext<FileCreatedMessage> context)
	{
		var message = context.Message;
		var command = new ComputeHashCommand(message.Id, message.Location);

		return _mediator.Send(command);
	}
}
