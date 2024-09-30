using System;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Domain.Abstractions;

public interface ISoftDeletable
{
	bool IsDeleted { get; }

	DateTimeOffset? DeletedAt { get; }

	UserId? DeletedById { get; }
}
