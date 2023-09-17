using FluentValidation;

namespace Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Models;

public class DbContextSettingsValidator : AbstractValidator<DbContextSettings>
{
	public DbContextSettingsValidator()
	{
		RuleFor(a => a.ProviderType)
			.NotEqual(DatabaseProviderType.Unknown);
	}
}
