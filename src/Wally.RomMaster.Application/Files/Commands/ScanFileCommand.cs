using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Application.Files.Commands;

[ExcludeFromCodeCoverage]
public class ScanFileCommand : ICommand
{
	public ScanFileCommand(FileLocation location)
	{
		Location = location;
	}

	public FileLocation Location { get; }
}
