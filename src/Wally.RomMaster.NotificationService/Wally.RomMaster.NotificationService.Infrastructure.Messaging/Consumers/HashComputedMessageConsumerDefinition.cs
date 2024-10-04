using System;
using MassTransit;

namespace Wally.RomMaster.NotificationService.Infrastructure.Messaging.Consumers;

public class HashComputedMessageConsumerDefinition : ConsumerDefinition<HashComputedMessageConsumer>
{
	public HashComputedMessageConsumerDefinition()
	{
		// limit the number of messages consumed concurrently
		// this applies to the consumer only, not the endpoint
		ConcurrentMessageLimit = 1;
	}

	protected override void ConfigureConsumer(
		IReceiveEndpointConfigurator endpointConfigurator,
		IConsumerConfigurator<HashComputedMessageConsumer> consumerConfigurator,
		IRegistrationContext context)
	{
		endpointConfigurator.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(60)));
		endpointConfigurator.UseInMemoryOutbox(context);
	}
}
