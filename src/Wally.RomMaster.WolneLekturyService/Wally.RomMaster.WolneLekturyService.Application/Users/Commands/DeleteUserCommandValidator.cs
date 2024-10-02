using FluentValidation;

namespace Wally.RomMaster.WolneLekturyService.Application.Users.Commands;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
	public DeleteUserCommandValidator()
	{
		RuleFor(a => a.UserId)
			.NotEmpty();
	}
}
