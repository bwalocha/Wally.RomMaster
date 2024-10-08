﻿using FluentValidation;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft.Models;

public class AuthenticationSettingsValidator : AbstractValidator<AuthenticationSettings>
{
	public AuthenticationSettingsValidator()
	{
		RuleFor(a => a.Authority)
			.NotEmpty();
		RuleFor(a => a.ClientId)
			.NotEmpty();
		RuleFor(a => a.ClientSecret)
			.NotEmpty();
	}
}
