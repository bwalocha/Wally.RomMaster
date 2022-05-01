using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.Application.Files.Commands;

[ExcludeFromCodeCoverage]
public class RemoveOutdatedFilesCommand : ICommand
{
	public RemoveOutdatedFilesCommand(DateTime timestamp)
	{
		Timestamp = timestamp;
	}

	public DateTime Timestamp { get; }
}
