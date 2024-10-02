using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.WolneLekturyService.Application.Abstractions;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Responses;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public sealed class GetUserQuery : IQuery<GetUserResponse>
{
	public GetUserQuery(UserId userId)
	{
		UserId = userId;
	}

	public UserId UserId { get; }
}
