using System;
using Wally.RomMaster.HashService.Domain.Abstractions;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Providers;

// TODO: use userId form JWT Token
public class HttpUserProvider : IUserProvider
{
	private readonly UserId _userId = new(Guid.Parse("AAAAAAAA-0000-0000-0000-ADD702D3016B"));

	public UserId GetCurrentUserId()
	{
		return _userId;
	}
}
