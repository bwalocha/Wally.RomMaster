using FluentValidation;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class ScanFileCommandValidator : AbstractValidator<ScanFileCommand>
{
	public ScanFileCommandValidator()
	{
		RuleFor(a => a.Location)
			.NotEmpty();
	}
}
