using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.HashService.Domain.Abstractions;

namespace Wally.RomMaster.HashService.Application.Abstractions;

public interface IDomainEventHandler<in TDomainEvent>
	where TDomainEvent : DomainEvent
{
	Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken);
}
