using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.DomainEvents;

namespace Wally.RomMaster.FileService.Domain.Files;

[ExcludeFromCodeCoverage]
public class FileModifiedDomainEvent : DomainEvent
{
	public FileModifiedDomainEvent(Guid id)
	{
		Id = id;
	}

	public Guid Id { get; }
}
