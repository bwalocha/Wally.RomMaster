﻿using System.Diagnostics.CodeAnalysis;
using Wally.RomMaster.NotificationService.Application.Abstractions;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Application.Users.Commands;

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
