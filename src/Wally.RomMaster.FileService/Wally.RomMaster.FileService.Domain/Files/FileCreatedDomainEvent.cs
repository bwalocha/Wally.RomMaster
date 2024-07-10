using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Domain.Files;

[ExcludeFromCodeCoverage]
public class FileCreatedDomainEvent : DomainEvent
{
	public FileCreatedDomainEvent(FileId fileId)
	{
		FileId = fileId;
	}

	public FileId FileId { get; }
}
