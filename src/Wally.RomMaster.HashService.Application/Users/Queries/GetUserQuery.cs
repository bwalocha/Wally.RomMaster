﻿using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Queries;
using Wally.RomMaster.HashService.Application.Contracts.Responses.Users;

namespace Wally.RomMaster.HashService.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public sealed class GetUserQuery : IQuery<GetUserResponse>
{
	public GetUserQuery(Guid id)
	{
		Id = id;
	}

	public Guid Id { get; }
}