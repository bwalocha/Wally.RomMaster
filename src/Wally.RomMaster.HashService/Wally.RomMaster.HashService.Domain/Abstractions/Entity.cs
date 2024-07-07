﻿using System.Collections.Generic;

namespace Wally.RomMaster.HashService.Domain.Abstractions;

public class Entity<TEntity, TStronglyTypedId> : IEntity
	where TEntity : Entity<TEntity, TStronglyTypedId>
	where TStronglyTypedId : new()
{
	private readonly List<DomainEvent> _domainEvents = new();

	protected Entity()
		: this(new TStronglyTypedId())
	{
	}

	protected Entity(TStronglyTypedId id)
	{
		Id = id;
	}

	public TStronglyTypedId Id { get; private set; }

	public IReadOnlyCollection<DomainEvent> GetDomainEvents()
	{
		return _domainEvents.AsReadOnly();
	}

	public void RemoveDomainEvent(DomainEvent domainEvent)
	{
		_domainEvents.Remove(domainEvent);
	}

	protected void AddDomainEvent(DomainEvent domainEvent)
	{
		_domainEvents.Add(domainEvent);
	}
}
