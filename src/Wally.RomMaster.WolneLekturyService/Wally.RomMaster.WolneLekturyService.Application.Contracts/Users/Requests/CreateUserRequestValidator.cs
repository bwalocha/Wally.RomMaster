using FluentValidation;

namespace Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Requests;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
	public CreateUserRequestValidator()
	{
		RuleFor(a => a.Name)
			.NotEmpty()
			.MaximumLength(256);
	}
}
