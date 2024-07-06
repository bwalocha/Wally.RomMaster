using System;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Domain.Abstractions;

public interface IAuditable
{
	DateTimeOffset CreatedAt { get; }

	UserId CreatedById { get; }

	DateTimeOffset? ModifiedAt { get; }

	UserId? ModifiedById { get; }
}
