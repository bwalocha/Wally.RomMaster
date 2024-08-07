﻿using System;
using Wally.RomMaster.FileService.Domain.Abstractions;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Tests.IntegrationTests.Helpers;

public class HttpUserProviderStub : IUserProvider
{
	private readonly UserId _userId = new(Guid.Parse("FFFFFFFF-0000-0000-0000-ADD702D3016B"));

	public UserId GetCurrentUserId()
	{
		return _userId;
	}
}
