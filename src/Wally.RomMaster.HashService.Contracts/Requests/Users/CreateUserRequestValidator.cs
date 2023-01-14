using FluentValidation;

namespace Wally.RomMaster.HashService.Contracts.Requests.Users;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
	public CreateUserRequestValidator()
	{
		RuleFor(a => a.Name)
			.NotEmpty()
			.MaximumLength(256);
	}
}
