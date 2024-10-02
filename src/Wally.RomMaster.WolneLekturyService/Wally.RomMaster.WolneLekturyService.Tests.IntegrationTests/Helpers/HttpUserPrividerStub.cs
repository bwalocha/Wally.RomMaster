using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Tests.IntegrationTests.Helpers;

public class HttpUserProviderStub : IUserProvider
{
	private readonly UserId _userId = new(Guid.Parse("FFFFFFFF-0000-0000-0000-ADD702D3016B"));

	public UserId GetCurrentUserId()
	{
		return _userId;
	}
}
