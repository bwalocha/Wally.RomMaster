using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.FileService.Application.Messages.Files;

namespace Wally.RomMaster.HashService.Infrastructure.Messaging.Consumers;

public class FileCreatedMessageFaultConsumer : IConsumer<Fault<FileCreatedMessage>>
{
	private readonly ILogger<FileCreatedMessageFaultConsumer> _logger;
	
	public FileCreatedMessageFaultConsumer(ILogger<FileCreatedMessageFaultConsumer> logger)
	{
		_logger = logger;
	}
	
	public Task Consume(ConsumeContext<Fault<FileCreatedMessage>> context)
	{
		_logger.LogCritical("Massage '{ContextMessage}' processing error: {ContextMessageId}", context.Message, context.MessageId);
		
		return Task.CompletedTask;
	}
}
