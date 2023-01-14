using System;
using System.Diagnostics.CodeAnalysis;

using Wally.RomMaster.HashService.Contracts.Responses.Users;
using Wally.Lib.DDD.Abstractions.Queries;

namespace Wally.RomMaster.HashService.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public class GetUserQuery : IQuery<GetUserResponse>
{
	public GetUserQuery(Guid id)
	{
		Id = id;
	}

	public Guid Id { get; }
}
