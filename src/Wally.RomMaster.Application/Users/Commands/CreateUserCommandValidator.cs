using System;

using FluentValidation;

namespace Wally.RomMaster.Application.Users.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator()
	{
		RuleFor(a => a.Id)
			.NotEmpty()
			.NotEqual(Guid.Empty);
		RuleFor(a => a.Name)
			.NotEmpty()
			.MinimumLength(1)
			.MaximumLength(256);
	}
}
