using System;
using MassTransit;

namespace Wally.RomMaster.NotificationService.Infrastructure.Messaging.Consumers;

public class FileModifiedMessageConsumerDefinition : ConsumerDefinition<FileModifiedMessageConsumer>
{
	public FileModifiedMessageConsumerDefinition()
	{
		// limit the number of messages consumed concurrently
		// this applies to the consumer only, not the endpoint
		ConcurrentMessageLimit = 4;
	}
	
	protected override void ConfigureConsumer(
		IReceiveEndpointConfigurator endpointConfigurator,
		IConsumerConfigurator<FileModifiedMessageConsumer> consumerConfigurator,
		IRegistrationContext context)
	{
		endpointConfigurator.CreatePartitioner(1);
		endpointConfigurator.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(60)));
		endpointConfigurator.UseInMemoryOutbox(context);
	}
}
