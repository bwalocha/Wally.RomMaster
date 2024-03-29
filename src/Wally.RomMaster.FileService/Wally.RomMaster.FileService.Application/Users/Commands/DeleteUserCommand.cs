using System.Diagnostics.CodeAnalysis;
using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Application.Users.Commands;

[ExcludeFromCodeCoverage]
public sealed class DeleteUserCommand : ICommand
{
	public DeleteUserCommand(UserId userId)
	{
		UserId = userId;
	}

	public UserId UserId { get; }
}
