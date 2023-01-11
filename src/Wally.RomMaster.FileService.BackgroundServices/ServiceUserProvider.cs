using System;

using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.BackgroundServices;

public class ServiceUserProvider : IUserProvider
{
	private readonly Guid _userId = Guid.Parse("AAAAAAAA-0000-0000-0000-ADD702D3016B");

	/*public Task<User> GetCurrentUserAsync(CancellationToken cancellationToken)
	{
		return Task.FromResult(User.Create(_userId, "ServiceUser"));
	}*/

	public Guid GetCurrentUserId()
	{
		return _userId;
	}
}
