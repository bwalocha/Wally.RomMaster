namespace Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Models;

public enum MessageBrokerType
{
	Unknown = 0,
	None,
	AzureServiceBus,
	RabbitMQ,
}
