﻿using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.HashService.Application.Abstractions;
using Wally.RomMaster.HashService.Domain.Files;

namespace Wally.RomMaster.HashService.Application.Hashes.Commands;

[ExcludeFromCodeCoverage]
public sealed class ComputeHashCommand : ICommand
{
	public ComputeHashCommand(FileId fileId, FileLocation fileLocation)
	{
		FileId = fileId;
		FileLocation = fileLocation;
	}
	
	public FileId FileId { get; }
	
	public FileLocation FileLocation { get; }
}
