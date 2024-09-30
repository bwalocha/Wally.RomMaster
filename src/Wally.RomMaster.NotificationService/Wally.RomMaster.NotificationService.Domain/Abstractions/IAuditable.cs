using System;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Domain.Abstractions;

public interface IAuditable
{
	DateTimeOffset CreatedAt { get; }

	UserId CreatedById { get; }

	DateTimeOffset? ModifiedAt { get; }

	UserId? ModifiedById { get; }
}
