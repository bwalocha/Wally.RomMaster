using System;

using FluentValidation;

namespace Wally.RomMaster.FileService.Application.Users.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator()
	{
		RuleFor(a => a.Id)
			.NotEmpty()
			.NotEqual(Guid.Empty);
		RuleFor(a => a.Name)
			.NotEmpty()
			.MaximumLength(256);
	}
}
