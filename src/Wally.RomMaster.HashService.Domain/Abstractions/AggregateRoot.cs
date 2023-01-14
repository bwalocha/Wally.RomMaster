﻿using System;

namespace Wally.RomMaster.HashService.Domain.Abstractions;

public abstract class AggregateRoot : Wally.Lib.DDD.Abstractions.DomainModels.AggregateRoot
{
	protected AggregateRoot()
	{
	}

	protected AggregateRoot(Guid id)
		: base(id)
	{
	}

	public DateTime CreatedAt { get; private set; }

	public Guid CreatedById { get; private set; }

	public DateTime? ModifiedAt { get; private set; }

	public Guid? ModifiedById { get; private set; }
}
