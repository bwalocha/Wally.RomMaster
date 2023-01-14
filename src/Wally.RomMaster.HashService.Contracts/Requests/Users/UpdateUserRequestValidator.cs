using FluentValidation;

namespace Wally.RomMaster.HashService.Contracts.Requests.Users;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
	public UpdateUserRequestValidator()
	{
		RuleFor(a => a.Name)
			.NotEmpty()
			.MaximumLength(256);
	}
}
