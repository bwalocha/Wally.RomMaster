using FluentValidation;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft.Models;

public class ConnectionStringsValidator : AbstractValidator<ConnectionStrings>
{
	public ConnectionStringsValidator()
	{
		RuleFor(a => a.Database)
			.NotEmpty();
		RuleFor(a => a.ServiceBus)
			.NotEmpty();
	}
}
