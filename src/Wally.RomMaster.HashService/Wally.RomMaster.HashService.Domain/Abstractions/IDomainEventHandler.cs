using System.Threading;
using System.Threading.Tasks;

namespace Wally.RomMaster.HashService.Domain.Abstractions;

public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : DomainEvent
{
	Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken);
}
