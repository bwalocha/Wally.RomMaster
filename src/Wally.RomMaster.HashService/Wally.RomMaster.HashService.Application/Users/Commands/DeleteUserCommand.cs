using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.HashService.Domain.Users;
using Wally.Lib.DDD.Abstractions.Commands;

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
