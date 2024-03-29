using System.Collections.Generic;
using Wally.Lib.DDD.Abstractions.DomainEvents;

namespace Wally.RomMaster.HashService.Domain.Abstractions;

public interface IEntity
{
	IReadOnlyCollection<DomainEvent> GetDomainEvents();

	void RemoveDomainEvent(DomainEvent domainEvent);
}
