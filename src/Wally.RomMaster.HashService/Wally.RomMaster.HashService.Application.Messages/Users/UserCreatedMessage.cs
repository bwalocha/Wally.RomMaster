﻿using System;

namespace Wally.RomMaster.HashService.Application.Messages.Users;

public class UserCreatedMessage
{
	public UserCreatedMessage(Guid id, string name)
	{
		Id = id;
		Name = name;

		new UserCreatedMessageValidator().Validate(this);
	}

	public Guid Id { get; }

	public string Name { get; }
}
