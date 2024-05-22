using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.HashService.Application.Contracts.Responses.Users;
using Wally.RomMaster.HashService.Domain.Users;
using Wally.Lib.DDD.Abstractions.Queries;

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
