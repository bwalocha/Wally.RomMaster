using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using Wally.RomMaster.FileService.Application.Messages.Users;
using Wally.RomMaster.FileService.Domain.Users;
using Wally.Lib.DDD.Abstractions.DomainEvents;

namespace Wally.RomMaster.FileService.Application.Users;

public class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
{
	private readonly IBus _bus;

	public UserCreatedDomainEventHandler(IBus bus)
	{
		_bus = bus;
	}

	public Task HandleAsync(UserCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		var message = new UserCreatedMessage(domainEvent.Id, domainEvent.Name);

		return _bus.Publish(message, cancellationToken);
	}
}
