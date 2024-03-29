using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.HashService.Application.Messages.Hashes;

namespace Wally.RomMaster.HashService.Application.Hashes.Commands;

public class ComputeHashCommandHandler : CommandHandler<ComputeHashCommand>
{
	private readonly IBus _bus;
	private readonly HashAlgorithm _hashAlgorithm;
	private readonly ILogger<ComputeHashCommandHandler> _logger;

	public ComputeHashCommandHandler(HashAlgorithm hashAlgorithm, IBus bus, ILogger<ComputeHashCommandHandler> logger)
	{
		_hashAlgorithm = hashAlgorithm;
		_bus = bus;
		_logger = logger;
	}

	public override async Task HandleAsync(ComputeHashCommand command, CancellationToken cancellationToken)
	{
		_logger.LogDebug("Computing Hashes for '{0}'...", command.FileLocation);

		await using var stream = File.Open(command.FileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);
		// var size = stream.Length;

		// MD5
		using var md5 = MD5.Create();
		var hash = await md5.ComputeHashAsync(stream, cancellationToken);
		var computedMd5 = BitConverter.ToString(hash)
			.Replace("-", null, true, CultureInfo.InvariantCulture);

		_logger.LogInformation("MD5 for '{0}': {1}", command.FileLocation, computedMd5);

		// CRC32
		stream.Seek(0, SeekOrigin.Begin);
		hash = await _hashAlgorithm.ComputeHashAsync(stream, cancellationToken);
		var computedCrc32 = BitConverter.ToString(hash)
			.Replace("-", null, true, CultureInfo.InvariantCulture);

		_logger.LogInformation("CRC32 for '{0}': {1}", command.FileLocation, computedCrc32);

		var message = new HashComputedMessage(command.FileId, computedCrc32, computedMd5);

		await _bus.Publish(message, cancellationToken);
	}
}
