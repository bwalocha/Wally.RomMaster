using FluentValidation;

namespace Wally.RomMaster.Application.Files.Commands;

public class ScanFileCommandValidator : AbstractValidator<ScanFileCommand>
{
	public ScanFileCommandValidator()
	{
		RuleFor(a => a.Location)
			.NotEmpty();
	}
}
