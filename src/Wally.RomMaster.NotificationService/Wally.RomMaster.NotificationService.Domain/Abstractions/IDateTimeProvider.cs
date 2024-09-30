using System;

namespace Wally.RomMaster.NotificationService.Domain.Abstractions;

public interface IDateTimeProvider
{
	public DateTimeOffset GetDateTime();
}
