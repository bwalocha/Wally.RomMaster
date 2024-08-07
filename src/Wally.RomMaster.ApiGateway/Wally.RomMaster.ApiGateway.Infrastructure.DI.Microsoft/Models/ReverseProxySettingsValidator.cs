using FluentValidation;

namespace Wally.RomMaster.ApiGateway.Infrastructure.DI.Microsoft.Models;

public class ReverseProxySettingsValidator : AbstractValidator<ReverseProxySettings>
{
	public ReverseProxySettingsValidator()
	{
		RuleForEach(a => a.Routes)
			.NotEmpty();
		RuleForEach(a => a.Clusters)
			.NotEmpty();
	}
}
