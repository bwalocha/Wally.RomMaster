using FluentValidation;

namespace Wally.RomMaster.HashService.Application.Users.Commands;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
	public UpdateUserCommandValidator()
	{
		RuleFor(a => a.UserId)
			.NotEmpty();
		
		RuleFor(a => a.Name)
			.NotEmpty()
			.MaximumLength(256);
	}
}
