using System.Threading.Tasks;

using MassTransit;

using MediatR;

using Wally.RomMaster.FileService.Application.Messages.Files;
using Wally.RomMaster.HashService.Application.Hashes.Commands;

namespace Wally.RomMaster.HashService.Infrastructure.Messaging.Consumers;

public class FileModifiedMessageConsumer : IConsumer<FileModifiedMessage>
{
	private readonly IMediator _mediator;

	public FileModifiedMessageConsumer(IMediator mediator)
	{
		_mediator = mediator;
	}

	public Task Consume(ConsumeContext<FileModifiedMessage> context)
	{
		var message = context.Message;
		var command = new ComputeHashCommand(message.Id, message.Location);

		return _mediator.Send(command);
	}
}
