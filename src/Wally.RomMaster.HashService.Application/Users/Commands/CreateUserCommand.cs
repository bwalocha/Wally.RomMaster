using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.HashService.Application.Users.Commands;

[ExcludeFromCodeCoverage]
public sealed class CreateUserCommand : ICommand
{
	public CreateUserCommand(Guid id, string name)
	{
		Id = id;
		Name = name;
	}

	public CreateUserCommand(string name)
		: this(Guid.NewGuid(), name)
	{
	}

	public Guid Id { get; }

	public string Name { get; }
}
