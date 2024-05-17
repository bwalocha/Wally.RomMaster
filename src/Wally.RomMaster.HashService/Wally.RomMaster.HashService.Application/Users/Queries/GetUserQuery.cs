using System.Diagnostics.CodeAnalysis;
using Wally.Lib.DDD.Abstractions.Queries;
using Wally.RomMaster.HashService.Application.Contracts.Responses.Users;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public sealed class GetUserQuery : IQuery<GetUserResponse>
{
	public GetUserQuery(UserId userId)
	{
		UserId = userId;
	}
	
	public UserId UserId { get; }
}
