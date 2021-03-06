using System;
using System.Diagnostics.CodeAnalysis;

using Wally.Lib.DDD.Abstractions.Commands;

namespace Wally.RomMaster.Application.Users.Commands;

[ExcludeFromCodeCoverage]
public class UpdateUserCommand : ICommand
{
	public UpdateUserCommand(Guid id, string name)
	{
		Id = id;
		Name = name;
	}

	public Guid Id { get; }

	public string Name { get; }
}
