using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Wally.RomMaster.NotificationService.Application.Abstractions;
using Wally.RomMaster.NotificationService.Application.Messages.Users;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Application.Users;

public class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
{
	private readonly IBus _bus;

	public UserCreatedDomainEventHandler(IBus bus)
	{
		_bus = bus;
	}

	public Task HandleAsync(UserCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		var message = new UserCreatedMessage(domainEvent.Id.Value, domainEvent.Name);

		return _bus.Publish(message, cancellationToken);
	}
}
