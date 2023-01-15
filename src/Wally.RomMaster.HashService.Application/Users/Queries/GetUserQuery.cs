using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Queries;
using Wally.RomMaster.HashService.Contracts.Responses.Users;

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
