using FluentValidation;

namespace Wally.RomMaster.NotificationService.Application.Users.Commands;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
	public DeleteUserCommandValidator()
	{
		RuleFor(a => a.UserId)
			.NotEmpty();
	}
}
