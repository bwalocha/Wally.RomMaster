using FluentValidation;

namespace Wally.RomMaster.NotificationService.Application.Notifications.Commands;

public class BroadcastNotificationCommandValidator : AbstractValidator<BroadcastNotificationCommand>
{
	public BroadcastNotificationCommandValidator()
	{
		RuleFor(a => a.Title)
			.NotEmpty();

		RuleFor(a => a.Content)
			.NotEmpty()
			.MaximumLength(1024);
	}
}
