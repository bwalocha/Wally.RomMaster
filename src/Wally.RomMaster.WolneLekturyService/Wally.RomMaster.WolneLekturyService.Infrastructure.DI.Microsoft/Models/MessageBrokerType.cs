namespace Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft.Models;

public enum MessageBrokerType
{
	Unknown = 0,
	None,
	AzureServiceBus,
	Kafka,
	RabbitMQ,
}
