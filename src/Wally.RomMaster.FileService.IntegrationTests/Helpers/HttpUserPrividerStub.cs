using System;

using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.IntegrationTests.Helpers;

public class HttpUserProviderStub : IUserProvider
{
	private readonly Guid _userId = Guid.Parse("FFFFFFFF-0000-0000-0000-AAAAAAAAAAAA");

	public Guid GetCurrentUserId()
	{
		return _userId;
	}
}
