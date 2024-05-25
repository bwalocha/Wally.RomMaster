using FluentValidation;

namespace Wally.RomMaster.FileService.Application.Contracts.Requests.Users;

public class GetUsersRequestValidator : AbstractValidator<GetUsersRequest>
{
	public GetUsersRequestValidator()
	{
		RuleFor(a => a.Id)
			.NotEmpty()
			.When(a => a.Id != null);
		
		RuleFor(a => a.Name)
			.NotEmpty()
			.MaximumLength(256)
			.When(a => a.Name != null);
	}
}
