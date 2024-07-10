using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

[ExcludeFromCodeCoverage]
public sealed class ScanFilesCommand : ICommand
{
	public ScanFilesCommand(FileLocation[] locations)
	{
		Locations = locations.AsReadOnly();
	}

	public IReadOnlyCollection<FileLocation> Locations { get; }
}
