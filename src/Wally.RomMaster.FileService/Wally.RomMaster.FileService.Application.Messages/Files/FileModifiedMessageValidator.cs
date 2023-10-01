using FluentValidation;

namespace Wally.RomMaster.FileService.Application.Messages.Files;

public class FileModifiedMessageValidator : AbstractValidator<FileModifiedMessage>
{
	public FileModifiedMessageValidator()
	{
		RuleFor(a => a.Id)
			.NotEmpty();
		RuleFor(a => a.Location)
			.NotEmpty()
			.MaximumLength(3000);
	}
}
