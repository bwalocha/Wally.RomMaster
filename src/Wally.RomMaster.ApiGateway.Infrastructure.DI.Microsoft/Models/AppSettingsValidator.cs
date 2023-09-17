﻿using FluentValidation;

namespace Wally.RomMaster.ApiGateway.Infrastructure.DI.Microsoft.Models;

public class AppSettingsValidator : AbstractValidator<AppSettings>
{
	public AppSettingsValidator()
	{
		RuleFor(a => a.Authentication)
			.NotEmpty()
			.SetValidator(new AuthenticationSettingsValidator());
		RuleFor(a => a.Cors)
			.NotNull();
	}
}
