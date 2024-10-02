using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.WolneLekturyService.Application.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Application.Users.Commands;

[ExcludeFromCodeCoverage]
public sealed class UpdateUserCommand : ICommand
{
	public UpdateUserCommand(UserId userId, string name)
	{
		UserId = userId;
		Name = name;
	}

	public UserId UserId { get; }

	public string Name { get; }
}
