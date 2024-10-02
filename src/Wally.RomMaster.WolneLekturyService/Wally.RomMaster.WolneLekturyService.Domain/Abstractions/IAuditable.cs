using System;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

public interface IAuditable
{
	DateTimeOffset CreatedAt { get; }

	UserId CreatedById { get; }

	DateTimeOffset? ModifiedAt { get; }

	UserId? ModifiedById { get; }
}
