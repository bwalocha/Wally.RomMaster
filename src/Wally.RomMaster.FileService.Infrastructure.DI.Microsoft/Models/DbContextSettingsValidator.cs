﻿using FluentValidation;

namespace Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Models;

public class DbContextSettingsValidator : AbstractValidator<DbContextSettings>
{
	public DbContextSettingsValidator()
	{
		RuleFor(a => a.ProviderType)
			.NotEqual(DatabaseProviderType.Unknown);
	}
}
