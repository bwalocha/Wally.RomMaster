using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Wally.RomMaster.HashService.Application.Messages.Hashes;

namespace Wally.RomMaster.NotificationService.Infrastructure.Messaging.Consumers;

public class HashComputedMessageConsumer : IConsumer<HashComputedMessage>
{
	private readonly IMediator _mediator;

	public HashComputedMessageConsumer(IMediator mediator)
	{
		_mediator = mediator;
	}

	public Task Consume(ConsumeContext<HashComputedMessage> context)
	{
		throw new NotSupportedException();
	}
}
