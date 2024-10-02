using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.WolneLekturyService.Application.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Application.Users.Commands;

[ExcludeFromCodeCoverage]
public sealed class DeleteUserCommand : ICommand
{
	public DeleteUserCommand(UserId userId)
	{
		UserId = userId;
	}

	public UserId UserId { get; }
}
