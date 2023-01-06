﻿using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Queries;
using Wally.RomMaster.FileService.Contracts.Responses.Users;

namespace Wally.RomMaster.FileService.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public class GetUserQuery : IQuery<GetUserResponse>
{
	public GetUserQuery(Guid id)
	{
		Id = id;
	}

	public Guid Id { get; }
}
