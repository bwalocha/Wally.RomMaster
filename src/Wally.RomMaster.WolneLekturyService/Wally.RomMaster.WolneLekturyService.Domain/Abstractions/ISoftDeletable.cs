using System;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

public interface ISoftDeletable
{
	bool IsDeleted { get; }

	DateTimeOffset? DeletedAt { get; }

	UserId? DeletedById { get; }
}
