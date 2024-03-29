using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Wally.RomMaster.FileService.Application.Messages.Files;
using Wally.RomMaster.HashService.Application.Hashes.Commands;

namespace Wally.RomMaster.HashService.Infrastructure.Messaging.Consumers;

public class FileModifiedMessageFaultConsumer : IConsumer<Fault<FileModifiedMessage>>
{
	private readonly ILogger<FileModifiedMessageFaultConsumer> _logger;

	public FileModifiedMessageFaultConsumer(ILogger<FileModifiedMessageFaultConsumer> logger)
	{
		_logger = logger;
	}

	public Task Consume(ConsumeContext<Fault<FileModifiedMessage>> context)
	{
		_logger.LogCritical($"Massage '{context.Message}' processing error: {context.MessageId}");
		
		return Task.CompletedTask;
	}
}
