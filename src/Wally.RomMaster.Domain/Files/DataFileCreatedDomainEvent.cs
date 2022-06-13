using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.DomainEvents;

namespace Wally.RomMaster.Domain.Files;

[ExcludeFromCodeCoverage]
public class DataFileCreatedDomainEvent : DomainEvent
{
	public DataFileCreatedDomainEvent(Guid id)
	{
		Id = id;
	}

	public Guid Id { get; }
}
