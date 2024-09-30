namespace Wally.RomMaster.NotificationService.Infrastructure.DI.Microsoft.Models;

public enum MessageBrokerType
{
	Unknown = 0,
	None,
	AzureServiceBus,
	Kafka,
	RabbitMQ,
}
