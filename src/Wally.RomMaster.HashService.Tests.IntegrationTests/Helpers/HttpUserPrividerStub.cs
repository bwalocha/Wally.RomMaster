﻿using System;

using Wally.RomMaster.HashService.Domain.Abstractions;

namespace Wally.RomMaster.HashService.Tests.IntegrationTests.Helpers;

public class HttpUserProviderStub : IUserProvider
{
	private readonly Guid _userId = Guid.Parse("FFFFFFFF-0000-0000-0000-ADD702D3016B");

	public Guid GetCurrentUserId()
	{
		return _userId;
	}
}