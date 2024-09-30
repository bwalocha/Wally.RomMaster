using System.Collections.Generic;

namespace Wally.RomMaster.NotificationService.Domain.Abstractions;

public interface IEntity
{
	IReadOnlyCollection<DomainEvent> GetDomainEvents();

	void RemoveDomainEvent(DomainEvent domainEvent);
}
