﻿using FluentValidation;

namespace Wally.RomMaster.HashService.Messages.Users;

public class UserCreatedMessageValidator : AbstractValidator<UserCreatedMessage>
{
	public UserCreatedMessageValidator()
	{
		RuleFor(a => a.Id)
			.NotEmpty();
		RuleFor(a => a.Name)
			.NotEmpty()
			.MaximumLength(256);
	}
}
