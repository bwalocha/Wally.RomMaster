using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.HashService.Application.Files;
using Wally.RomMaster.HashService.Application.Messages.Hashes;

namespace Wally.RomMaster.HashService.Application.Hashes.Commands;

public class ComputeHashCommandHandler : CommandHandler<ComputeHashCommand>
{
	private readonly IBus _bus;
	private readonly IFileRepository _repository;
	private readonly HashAlgorithm _hashAlgorithm;
	private readonly ILogger<ComputeHashCommandHandler> _logger;
	
	public ComputeHashCommandHandler(IFileRepository repository, HashAlgorithm hashAlgorithm, IBus bus, ILogger<ComputeHashCommandHandler> logger)
	{
		_repository = repository;
		_hashAlgorithm = hashAlgorithm;
		_bus = bus;
		_logger = logger;
	}
	
	public override async Task HandleAsync(ComputeHashCommand command, CancellationToken cancellationToken)
	{
		var file = await _repository.GetOrDefaultAsync(command.FileId, cancellationToken);

		if (!System.IO.File.Exists(command.FileLocation.Value.LocalPath))
		{
			_logger.LogWarning("File not found '{FileLocation}'...", command.FileLocation.Value);
			return;	
		}
		
		var fileInfo = new FileInfo(command.FileLocation.Value.LocalPath);
		if (file is null)
		{
			file = Domain.Files.File.Create(command.FileId, fileInfo);
			
			_logger.LogDebug("Computing Hashes for '{FileLocation}'...", command.FileLocation.Value);

			var hashes = await ComputeHashesAsync(fileInfo, new[] { MD5.Create(), _hashAlgorithm, }, cancellationToken);

			file.SetMd5(hashes[0])
				.SetCrc32(hashes[1]);
			
			_repository.Add(file);
		}
		
		if (file.HasChanged(fileInfo))
		{
			_logger.LogDebug("Computing Hashes for '{FileLocation}'...", command.FileLocation.Value);

			var hashes = await ComputeHashesAsync(fileInfo, new[] { MD5.Create(), _hashAlgorithm, }, cancellationToken);

			file.Update(fileInfo)
				.SetMd5(hashes[0])
				.SetCrc32(hashes[1]);
			
			_repository.Update(file);
		}

		var message = new HashComputedMessage(command.FileId.Value, file.Crc32!, file.Md5!);

		await _bus.Publish(message, cancellationToken);
	}

	private async Task<string[]> ComputeHashesAsync(FileInfo fileInfo, HashAlgorithm[] hashAlgorithms, CancellationToken cancellationToken)
	{
		var buffer = new byte[8192];
		int bytesRead;
		long totalBytesRead = 0;
		Array.ForEach(hashAlgorithms, a => a.Initialize());
		
		await using var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: buffer.Length, useAsync: true);
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
				_logger.LogInformation("HASH for '{FilePath}': {Progress:0.00}% ({TotalBytesRead}/{FileLength})", fileInfo.FullName, progress, totalBytesRead, fileLength);
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
