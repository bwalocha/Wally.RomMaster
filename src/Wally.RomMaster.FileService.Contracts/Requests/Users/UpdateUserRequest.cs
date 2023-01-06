﻿using Wally.Lib.DDD.Abstractions.Requests;

namespace Wally.RomMaster.FileService.Contracts.Requests.Users;

[ExcludeFromCodeCoverage]
public class UpdateUserRequest : IRequest
{
	public UpdateUserRequest(string name)
	{
		Name = name;
	}

	public string Name { get; }
}
