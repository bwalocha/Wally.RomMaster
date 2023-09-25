using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.HashService.Application.Hashes.Commands;

[ExcludeFromCodeCoverage]
public sealed class ComputeHashCommand : ICommand
{
	public ComputeHashCommand(Guid fileId, string fileLocation)
	{
		FileId = fileId;
		FileLocation = fileLocation;
	}

	public Guid FileId { get; }

	public string FileLocation { get; }
}
