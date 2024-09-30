using System;
using Wally.RomMaster.NotificationService.Domain.Abstractions;

namespace Wally.RomMaster.NotificationService.Domain.Users;

public class UserId : GuidId<UserId>
{
	public UserId()
	{
	}

	public UserId(Guid value)
		: base(value)
	{
	}
}
