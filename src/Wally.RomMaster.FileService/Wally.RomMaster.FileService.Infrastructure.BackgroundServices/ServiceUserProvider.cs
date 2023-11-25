using System;
using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Infrastructure.BackgroundServices;

public class ServiceUserProvider : IUserProvider
{
	private readonly UserId _userId = new(Guid.Parse("AAAAAAAA-0000-0000-0000-ADD702D3016B"));

	/*public Task<User> GetCurrentUserAsync(CancellationToken cancellationToken)
	{
		return Task.FromResult(User.Create(_userId, "ServiceUser"));
	}*/

	public UserId GetCurrentUserId()
	{
		return _userId;
	}
}
