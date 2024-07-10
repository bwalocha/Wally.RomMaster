using System;
using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.FileService.Application.Abstractions;

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
