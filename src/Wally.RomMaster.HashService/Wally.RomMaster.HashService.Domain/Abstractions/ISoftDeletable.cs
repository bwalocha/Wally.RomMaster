using System;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Domain.Abstractions;

public interface ISoftDeletable
{
	bool IsDeleted { get; }

	DateTimeOffset? DeletedAt { get; }

	UserId? DeletedById { get; }
}
