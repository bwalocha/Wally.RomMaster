using FluentValidation;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class ScanFilesCommandValidator : AbstractValidator<ScanFilesCommand>
{
	public ScanFilesCommandValidator()
	{
		RuleFor(a => a.Locations)
			.NotNull();
	}
}
