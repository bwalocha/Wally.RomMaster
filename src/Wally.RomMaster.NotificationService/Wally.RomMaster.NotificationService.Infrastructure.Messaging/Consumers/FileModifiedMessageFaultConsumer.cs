using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.FileService.Application.Messages.Files;

namespace Wally.RomMaster.NotificationService.Infrastructure.Messaging.Consumers;

public class FileModifiedMessageFaultConsumer : IConsumer<Fault<FileModifiedMessage>>
{
	private readonly ILogger<FileModifiedMessageFaultConsumer> _logger;
	
	public FileModifiedMessageFaultConsumer(ILogger<FileModifiedMessageFaultConsumer> logger)
	{
		_logger = logger;
	}
	
	public Task Consume(ConsumeContext<Fault<FileModifiedMessage>> context)
	{
		_logger.LogCritical("Massage '{ContextMessage}' processing error: {ContextMessageId}", context.Message, context.MessageId);
		
		return Task.CompletedTask;
	}
}
