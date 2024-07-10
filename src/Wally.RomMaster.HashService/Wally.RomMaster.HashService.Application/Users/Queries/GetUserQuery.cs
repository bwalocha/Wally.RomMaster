using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.HashService.Application.Abstractions;
using Wally.RomMaster.HashService.Application.Contracts.Users.Responses;
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
