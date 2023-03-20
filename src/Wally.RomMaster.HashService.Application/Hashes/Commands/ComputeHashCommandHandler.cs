using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.Lib.ServiceBus.Abstractions;
using Wally.RomMaster.HashService.Application.Messages.Hashes;

namespace Wally.RomMaster.HashService.Application.Hashes.Commands;

public class ComputeHashCommandHandler : CommandHandler<ComputeHashCommand>
{
	private readonly HashAlgorithm _hashAlgorithm;
	private readonly ILogger<ComputeHashCommandHandler> _logger;
	private readonly IPublisher _publisher;

	public ComputeHashCommandHandler(
		HashAlgorithm hashAlgorithm,
		IPublisher publisher,
		ILogger<ComputeHashCommandHandler> logger)
	{
		_hashAlgorithm = hashAlgorithm;
		_publisher = publisher;
		_logger = logger;
	}

	public override async Task HandleAsync(ComputeHashCommand command, CancellationToken cancellationToken)
	{
		await using var stream = File.Open(command.FileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);
		var size = stream.Length;
		var hash = await _hashAlgorithm.ComputeHashAsync(stream, cancellationToken);
		var computedCrc32 = BitConverter.ToString(hash)
			.Replace("-", null, true, CultureInfo.InvariantCulture);

		_logger.LogInformation("CRC32 for '{0}': {1}", command.FileLocation, computedCrc32);

		var message = new HashComputedMessage(command.FileId, computedCrc32);
		await _publisher.PublishAsync(message, cancellationToken);
	}
}
