using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.NotificationService.Application.Abstractions;
using Wally.RomMaster.NotificationService.Application.Contracts.Users.Responses;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Application.Users.Queries;

[ExcludeFromCodeCoverage]
public sealed class GetUserQuery : IQuery<GetUserResponse>
{
	public GetUserQuery(UserId userId)
	{
		UserId = userId;
	}

	public UserId UserId { get; }
}
