using FluentValidation;

namespace Wally.RomMaster.HashService.Application.Users.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator()
	{
		RuleFor(a => a.Id)
			.NotEmpty();
		RuleFor(a => a.Name)
			.NotEmpty()
			.MaximumLength(256);
	}
}
