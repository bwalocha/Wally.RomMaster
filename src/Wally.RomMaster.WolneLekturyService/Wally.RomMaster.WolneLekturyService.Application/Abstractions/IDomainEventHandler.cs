using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Application.Abstractions;

public interface IDomainEventHandler<in TDomainEvent>
	where TDomainEvent : DomainEvent
{
	Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken);
}
