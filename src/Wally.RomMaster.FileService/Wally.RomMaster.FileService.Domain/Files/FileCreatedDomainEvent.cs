using System.Diagnostics.CodeAnalysis;
using Wally.Lib.DDD.Abstractions.DomainEvents;

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
