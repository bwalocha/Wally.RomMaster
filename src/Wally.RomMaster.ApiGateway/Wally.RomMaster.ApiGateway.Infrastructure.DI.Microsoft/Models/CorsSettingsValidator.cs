using FluentValidation;

namespace Wally.RomMaster.ApiGateway.Infrastructure.DI.Microsoft.Models;

public class CorsSettingsValidator : AbstractValidator<CorsSettings>
{
	public CorsSettingsValidator()
	{
		RuleForEach(a => a.Origins)
			.NotEmpty();
	}
}
