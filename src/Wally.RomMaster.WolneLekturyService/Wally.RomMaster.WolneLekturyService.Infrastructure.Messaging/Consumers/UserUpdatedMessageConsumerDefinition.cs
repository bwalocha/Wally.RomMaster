using System;
using MassTransit;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.Messaging.Consumers;

public class UserUpdatedMessageConsumerDefinition : ConsumerDefinition<UserUpdatedMessageConsumer>
{
	public UserUpdatedMessageConsumerDefinition()
	{
		// limit the number of messages consumed concurrently
		// this applies to the consumer only, not the endpoint
		ConcurrentMessageLimit = 4;
	}

	protected override void ConfigureConsumer(
		IReceiveEndpointConfigurator endpointConfigurator,
		IConsumerConfigurator<UserUpdatedMessageConsumer> consumerConfigurator,
		IRegistrationContext context)
	{
		endpointConfigurator.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(60)));
		endpointConfigurator.UseInMemoryOutbox(context);
	}
}
