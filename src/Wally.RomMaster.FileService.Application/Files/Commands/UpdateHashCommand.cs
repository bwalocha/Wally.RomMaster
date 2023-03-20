using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

[ExcludeFromCodeCoverage]
public sealed class UpdateHashCommand : ICommand
{
	public UpdateHashCommand(Guid fileId, string crc32)
	{
		FileId = fileId;
		Crc32 = crc32;
	}

	public Guid FileId { get; }

	public string Crc32 { get; }
}
