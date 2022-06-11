using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.DomainEvents;

namespace Wally.RomMaster.Domain.Files;

[ExcludeFromCodeCoverage]
public class MetadataFileCreatedDomainEvent : DomainEvent
{
	public MetadataFileCreatedDomainEvent(Guid id)
	{
		Id = id;
	}

	public Guid Id { get; }
}
