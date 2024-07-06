using System;
using System.Globalization;
using System.IO;
using System.Linq;
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
		
		/*await using var stream = File.Open(command.FileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);
		
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
		
		_logger.LogInformation("CRC32 for '{0}': {1}", command.FileLocation, computedCrc32);*/
		
		//

		var hashes = await ComputeHashesAsync(command.FileLocation, new[] { MD5.Create(), _hashAlgorithm, }, cancellationToken);
		
		/*if (computedMd5 != hashes[0])
		{
			throw new InvalidDataException($"MD5 error: {computedMd5} <> {hashes[0]}");
		}

		if (computedCrc32 != hashes[1])
		{
			throw new InvalidDataException($"CRC32 error {computedCrc32} <> {hashes[1]}");
		}*/

		//
		
		var message = new HashComputedMessage(command.FileId, hashes[1], hashes[0]);
		
		await _bus.Publish(message, cancellationToken);
	}

	private async Task<string[]> ComputeHashesAsync(string filePath, HashAlgorithm[] hashAlgorithms, CancellationToken cancellationToken)
	{
		var buffer = new byte[8192];
		int bytesRead;
		long totalBytesRead = 0;
		Array.ForEach(hashAlgorithms, a => a.Initialize());
		
		await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: buffer.Length, useAsync: true);
		var fileLength = stream.Length;

		var lastProgressIndicator = 0d;
		while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
		{
			Array.ForEach(hashAlgorithms, a => a.TransformBlock(buffer, 0, bytesRead, null, 0));
			totalBytesRead += bytesRead;
			var progress = (double)totalBytesRead / fileLength * 100;
			if (Math.Abs(progress - lastProgressIndicator) > 1 || (int)progress == 100)
			{
				lastProgressIndicator = progress;
				_logger.LogInformation("HASH for '{FilePath}': {Progress:0.00}% ({TotalBytesRead}/{FileLength})", filePath, progress, totalBytesRead, fileLength);
			}
		}

		// Finalize the hash computation
		Array.ForEach(hashAlgorithms, a => a.TransformFinalBlock([], 0, 0));

		// Convert the byte array to a hexadecimal string
		return hashAlgorithms
			.Select(a => BitConverter.ToString(a.Hash!).Replace("-", "").ToUpperInvariant())
			.ToArray();
	}
}
