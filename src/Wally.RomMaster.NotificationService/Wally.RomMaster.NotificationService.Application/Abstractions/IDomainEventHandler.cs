using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.NotificationService.Domain.Abstractions;

namespace Wally.RomMaster.NotificationService.Application.Abstractions;

public interface IDomainEventHandler<in TDomainEvent>
	where TDomainEvent : DomainEvent
{
	Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken);
}
