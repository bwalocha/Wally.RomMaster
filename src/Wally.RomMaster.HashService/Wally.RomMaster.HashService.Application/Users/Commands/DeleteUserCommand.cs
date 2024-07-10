using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.HashService.Application.Abstractions;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Application.Users.Commands;

[ExcludeFromCodeCoverage]
public sealed class DeleteUserCommand : ICommand
{
	public DeleteUserCommand(UserId userId)
	{
		UserId = userId;
	}

	public UserId UserId { get; }
}
