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
		
		var computedMd5Check = await ComputeHashAsync(MD5.Create(), command.FileLocation, cancellationToken);

		_logger.LogInformation("MD5 for '{0}': {1}", command.FileLocation, computedMd5);
		_logger.LogInformation("MD5 for '{0}': {1} Check", command.FileLocation, computedMd5Check);

		if (computedMd5 != computedMd5Check)
		{
			throw new Exception("MD5 error");
		}
		
		// CRC32
		stream.Seek(0, SeekOrigin.Begin);
		hash = await _hashAlgorithm.ComputeHashAsync(stream, cancellationToken);
		var computedCrc32 = BitConverter.ToString(hash)
			.Replace("-", null, true, CultureInfo.InvariantCulture);
		
		var computedCrc32Check = await ComputeHashAsync(_hashAlgorithm, command.FileLocation, cancellationToken);
		
		_logger.LogInformation("CRC32 for '{0}': {1}", command.FileLocation, computedCrc32);
		_logger.LogInformation("CRC32 for '{0}': {1} Check", command.FileLocation, computedCrc32Check);
		
		if (computedCrc32 != computedCrc32Check)
		{
			throw new Exception("CRC32 error");
		}
		
		var message = new HashComputedMessage(command.FileId, computedCrc32, computedMd5);
		
		await _bus.Publish(message, cancellationToken);
	}

	private async Task<string> ComputeHashAsync(HashAlgorithm hashAlgorithm, string filePath, CancellationToken cancellationToken)
	{
		var buffer = new byte[8192];
		int bytesRead;
		long totalBytesRead = 0;

		await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: buffer.Length, useAsync: true);
		var fileLength = stream.Length;

		var lastProgressIndicator = 0L;
		while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
		{
			hashAlgorithm.TransformBlock(buffer, 0, bytesRead, null, 0);
			totalBytesRead += bytesRead;
			var progress = totalBytesRead / fileLength * 100;
			if (Math.Abs(progress - lastProgressIndicator) > 1 || (int)progress == 100)
			{
				lastProgressIndicator = progress;
				_logger.LogInformation("MD5 for '{FilePath}': {Progress:0.00}%", filePath, progress);
			}
		}

		// Finalize the hash computation
		hashAlgorithm.TransformFinalBlock([], 0, 0);

		// Convert the byte array to a hexadecimal string
		return BitConverter.ToString(hashAlgorithm.Hash!).Replace("-", "").ToLowerInvariant();
	}
	
	/*private async Task<string> ComputeCrc32Async(string filePath, CancellationToken cancellationToken)
	{
		var buffer = new byte[8192];
		int bytesRead;
		long totalBytesRead = 0;

		using var md5 = MD5.Create();
		await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: buffer.Length, useAsync: true);
		var fileLength = stream.Length;

		var lastProgressIndicator = 0L;
		while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
		{
			md5.TransformBlock(buffer, 0, bytesRead, null, 0);
			totalBytesRead += bytesRead;
			var progress = totalBytesRead / fileLength * 100;
			if (Math.Abs(progress - lastProgressIndicator) > 1 || (int)progress == 100)
			{
				lastProgressIndicator = progress;
				_logger.LogInformation("MD5 for '{FilePath}': {Progress:0.00}%", filePath, progress);
			}
		}

		// Finalize the hash computation
		md5.TransformFinalBlock([], 0, 0);

		// Convert the byte array to a hexadecimal string
		string checksum = BitConverter.ToString(md5.Hash!).Replace("-", "").ToLowerInvariant();
		
		return checksum;
	}*/
}
