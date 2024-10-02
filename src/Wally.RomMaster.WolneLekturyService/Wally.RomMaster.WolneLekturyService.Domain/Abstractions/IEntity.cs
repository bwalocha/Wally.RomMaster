using System.Collections.Generic;

namespace Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

public interface IEntity
{
	IReadOnlyCollection<DomainEvent> GetDomainEvents();

	void RemoveDomainEvent(DomainEvent domainEvent);
}
