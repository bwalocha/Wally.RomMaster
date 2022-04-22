using System.Threading;
using System.Threading.Tasks;

using Wally.Lib.DDD.Abstractions.DomainEvents;
using Wally.Lib.ServiceBus.Abstractions;
using Wally.RomMaster.Domain.Users;

namespace Wally.RomMaster.Application.Users;

public class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
{
	private readonly IPublisher _publisher;

	public UserCreatedDomainEventHandler(IPublisher publisher)
	{
		_publisher = publisher;
	}

	public Task HandleAsync(UserCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
	{
		return _publisher.PublishAsync(domainEvent, cancellationToken);
	}
}
