using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Commands;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Application.Users.Commands;

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
