using FluentValidation;

namespace Wally.RomMaster.WolneLekturyService.Application.Users.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator()
	{
		RuleFor(a => a.UserId)
			.NotEmpty();

		RuleFor(a => a.Name)
			.NotEmpty()
			.MaximumLength(256);
	}
}
