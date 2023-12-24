using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.FileService.Application.Messages.Files;
using Wally.RomMaster.HashService.Application.Hashes.Commands;

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
		_logger.LogCritical($"Massage '{context.Message}' processing error: {context.MessageId}");
		
		return Task.CompletedTask;
	}
}
