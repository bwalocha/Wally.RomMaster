using FluentValidation;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class ScanPathCommandValidator : AbstractValidator<ScanPathCommand>
{
	public ScanPathCommandValidator()
	{
		RuleFor(a => a.Location)
			.NotEmpty();
	}
}
