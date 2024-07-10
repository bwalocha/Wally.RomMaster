using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Domain.Files;

[ExcludeFromCodeCoverage]
public class FileModifiedDomainEvent : DomainEvent
{
	public FileModifiedDomainEvent(FileId fileId)
	{
		FileId = fileId;
	}

	public FileId FileId { get; }
}
