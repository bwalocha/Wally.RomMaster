using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.NotificationService.Application.Abstractions;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Application.Users.Commands;

[ExcludeFromCodeCoverage]
public sealed class DeleteUserCommand : ICommand
{
	public DeleteUserCommand(UserId userId)
	{
		UserId = userId;
	}

	public UserId UserId { get; }
}
