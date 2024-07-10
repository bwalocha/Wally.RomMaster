using System.Collections.Generic;

namespace Wally.RomMaster.FileService.Domain.Abstractions;

public interface IEntity
{
	IReadOnlyCollection<DomainEvent> GetDomainEvents();

	void RemoveDomainEvent(DomainEvent domainEvent);
}
