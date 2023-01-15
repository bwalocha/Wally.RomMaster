using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.HashService.Application.Hashes.Commands;

public class ComputeHashCommandHandler : CommandHandler<ComputeHashCommand>
{
	private readonly HashAlgorithm _hashAlgorithm;
	private readonly ILogger<ComputeHashCommandHandler> _logger;

	public ComputeHashCommandHandler(HashAlgorithm hashAlgorithm, ILogger<ComputeHashCommandHandler> logger)
	{
		_hashAlgorithm = hashAlgorithm;
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
	}
}
