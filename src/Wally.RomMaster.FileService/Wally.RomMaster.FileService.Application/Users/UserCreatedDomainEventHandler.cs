﻿using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Application.Messages.Users;
using Wally.RomMaster.FileService.Domain.Users;

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
		var message = new UserCreatedMessage(domainEvent.Id.Value, domainEvent.Name);

		return _bus.Publish(message, cancellationToken);
	}
}
