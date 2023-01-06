using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

// [DebuggerDisplay("{SourceType}: {Location}")]
[DebuggerDisplay("{Location}")]
[ExcludeFromCodeCoverage]
public class ScanFileCommand : ICommand
{
	public ScanFileCommand( /*SourceType sourceType,*/ FileLocation location)
	{
		// SourceType = sourceType;
		Location = location;
	}

	// public SourceType SourceType { get; }

	public FileLocation Location { get; }
}
