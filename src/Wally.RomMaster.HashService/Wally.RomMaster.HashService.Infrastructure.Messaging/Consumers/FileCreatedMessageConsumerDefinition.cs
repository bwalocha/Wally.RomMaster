using System;
using MassTransit;

namespace Wally.RomMaster.HashService.Infrastructure.Messaging.Consumers;

public class FileCreatedMessageConsumerDefinition : ConsumerDefinition<FileCreatedMessageConsumer>
{
	public FileCreatedMessageConsumerDefinition()
	{
		// limit the number of messages consumed concurrently
		// this applies to the consumer only, not the endpoint
		ConcurrentMessageLimit = 4;
	}
	
	protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
		IConsumerConfigurator<FileCreatedMessageConsumer> consumerConfigurator,
		IRegistrationContext context)
	{
		endpointConfigurator.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(60)));
		endpointConfigurator.UseInMemoryOutbox(context);
	}
}
