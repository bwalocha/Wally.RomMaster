using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

[DebuggerDisplay("{Location}")]
[ExcludeFromCodeCoverage]
public sealed class ScanFileCommand : ICommand
{
	public ScanFileCommand(FileLocation location)
	{
		Location = location;
	}

	public FileLocation Location { get; }
}
