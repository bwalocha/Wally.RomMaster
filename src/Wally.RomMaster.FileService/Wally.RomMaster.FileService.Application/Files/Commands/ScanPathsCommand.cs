using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

// [DebuggerDisplay("{SourceType}: {Location}")]
// [DebuggerDisplay("{Location}")]
[ExcludeFromCodeCoverage]
public sealed class ScanPathsCommand : ICommand
{
	public ScanPathsCommand(FileLocation[] locations)
	{
		Locations = locations.AsReadOnly();
	}

	public IReadOnlyCollection<FileLocation> Locations { get; }
}
