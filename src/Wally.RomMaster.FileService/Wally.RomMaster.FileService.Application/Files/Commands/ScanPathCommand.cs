using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

// [DebuggerDisplay("{SourceType}: {Location}")]
[DebuggerDisplay("{Location}")]
[ExcludeFromCodeCoverage]
public sealed class ScanPathCommand : ICommand
{
	public ScanPathCommand(FileLocation location)
	{
		Location = location;
	}

	public FileLocation Location { get; }
}
