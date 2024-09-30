using System;
using Wally.RomMaster.NotificationService.Domain.Abstractions;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Infrastructure.BackgroundServices.Providers;

public class ServiceUserProvider : IUserProvider
{
	private readonly UserId _userId = new(Guid.Parse("AAAAAAAA-0000-0000-0000-ADD702D3016B"));

	public UserId GetCurrentUserId()
	{
		return _userId;
	}
}
