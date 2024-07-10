using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.FileService.Application.Abstractions;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Application.Users.Commands;

[ExcludeFromCodeCoverage]
public sealed class CreateUserCommand : ICommand
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
