﻿using FluentValidation;

namespace Wally.RomMaster.WolneLekturyService.Application.Messages.Users;

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
