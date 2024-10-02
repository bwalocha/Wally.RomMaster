using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.WolneLekturyService.Application.Abstractions;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Application.Users.Commands;

[ExcludeFromCodeCoverage]
public sealed class CreateUserCommand : ICommand<UserId>
{
	public CreateUserCommand(UserId userId, string name)
	{
		UserId = userId;
		Name = name;
	}

	public CreateUserCommand(string name)
		: this(new UserId(), name)
	{
	}

	public UserId UserId { get; }

	public string Name { get; }
}
