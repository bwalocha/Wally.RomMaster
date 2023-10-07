using FluentValidation;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class ScanPathsCommandValidator : AbstractValidator<ScanPathsCommand>
{
	public ScanPathsCommandValidator()
	{
		RuleFor(a => a.Locations)
			.NotEmpty();
	}
}
