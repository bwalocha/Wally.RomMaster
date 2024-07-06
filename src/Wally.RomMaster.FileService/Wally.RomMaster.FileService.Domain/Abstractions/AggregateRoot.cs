using System;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Domain.Abstractions;

public class AggregateRoot<TAggregateRoot, TStronglyTypedId>
	: Entity<TAggregateRoot, TStronglyTypedId>, IAggregateRoot
	where TAggregateRoot : AggregateRoot<TAggregateRoot, TStronglyTypedId>
	where TStronglyTypedId : new()
{
	protected AggregateRoot()
	{
	}

	protected AggregateRoot(TStronglyTypedId id)
		: base(id)
	{
	}

	public DateTimeOffset CreatedAt { get; private set; } = default;

	public UserId CreatedById { get; private set; } = null!;

	public DateTimeOffset? ModifiedAt { get; private set; } = null;

	protected void SetModifiedAt(DateTime modifiedAt)
	{
		ModifiedAt = modifiedAt;
	}
	
	public UserId? ModifiedById { get; private set; } = null;
}
