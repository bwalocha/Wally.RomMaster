using FluentValidation;

namespace Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Requests;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
	public UpdateUserRequestValidator()
	{
		RuleFor(a => a.Name)
			.NotEmpty()
			.MaximumLength(256);
	}
}
