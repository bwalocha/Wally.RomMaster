using FluentValidation;

namespace Wally.RomMaster.HashService.Application.Hashes.Commands;

public class ComputeHashCommandValidator : AbstractValidator<ComputeHashCommand>
{
	public ComputeHashCommandValidator()
	{
		RuleFor(a => a.FileId)
			.NotEmpty();
		RuleFor(a => a.FileLocation)
			.NotEmpty()
			.MaximumLength(3000);
	}
}
