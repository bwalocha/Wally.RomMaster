using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

[ExcludeFromCodeCoverage]
public sealed class RemoveOutdatedFilesCommand : ICommand
{
	public RemoveOutdatedFilesCommand(DateTime timestamp)
	{
		Timestamp = timestamp;
	}

	public DateTime Timestamp { get; }
}
