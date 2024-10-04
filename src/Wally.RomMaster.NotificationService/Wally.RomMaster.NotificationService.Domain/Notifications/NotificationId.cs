using System;
using Wally.RomMaster.NotificationService.Domain.Abstractions;

namespace Wally.RomMaster.NotificationService.Domain.Notifications;

public class NotificationId : GuidId<NotificationId>
{
	public NotificationId()
	{
	}

	public NotificationId(Guid value)
		: base(value)
	{
	}
}
