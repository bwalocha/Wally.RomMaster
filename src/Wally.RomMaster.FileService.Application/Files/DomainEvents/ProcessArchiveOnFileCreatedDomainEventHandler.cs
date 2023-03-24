﻿using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;

using File = Wally.RomMaster.FileService.Domain.Files.File;

namespace Wally.RomMaster.FileService.Application.Files.DomainEvents;

public class ProcessArchiveOnFileCreatedDomainEventHandler : IDomainEventHandler<FileCreatedDomainEvent>
{
	private readonly IClockService _clockService;
	private readonly IFileRepository _fileRepository;
	private readonly ILogger<ProcessArchiveOnFileCreatedDomainEventHandler> _logger;

	public ProcessArchiveOnFileCreatedDomainEventHandler(
		IFileRepository fileRepository,
		IClockService clockService,
		ILogger<ProcessArchiveOnFileCreatedDomainEventHandler> logger)
	{
		_fileRepository = fileRepository;
		_clockService = clockService;
		_logger = logger;
	}

	public async Task HandleAsync(FileCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		var model = await _fileRepository.GetAsync(domainEvent.Id, cancellationToken);

		if (model.IsArchivePackage())
		{
			await using (var zipToOpen = new FileStream(
							model.Location.Location.LocalPath,
							FileMode.Open,
							FileAccess.Read))
			{
				try
				{
					using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read);
					foreach (var entry in archive.Entries)
					{
						var file = File.Create(_clockService, model.Path, model, entry);

						_fileRepository.Add(file);
					}
				}
				catch (Exception e)
				{
					// model.Error
					_logger.LogError("Error: '{0}'", e);
				}
			}
		}
	}
}