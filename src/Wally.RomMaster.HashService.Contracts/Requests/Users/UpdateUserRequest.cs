﻿using Wally.Lib.DDD.Abstractions.Requests;

namespace Wally.RomMaster.HashService.Contracts.Requests.Users;

[ExcludeFromCodeCoverage]
public class UpdateUserRequest : IRequest
{
	public UpdateUserRequest(string name)
	{
		Name = name;
	}

	public string Name { get; }
}
