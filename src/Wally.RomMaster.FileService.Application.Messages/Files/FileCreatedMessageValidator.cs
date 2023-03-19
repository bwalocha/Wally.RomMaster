using FluentValidation;

namespace Wally.RomMaster.FileService.Application.Messages.Files;

public class FileCreatedMessageValidator : AbstractValidator<FileCreatedMessage>
{
	public FileCreatedMessageValidator()
	{
		RuleFor(a => a.Id)
			.NotEmpty();
		RuleFor(a => a.Location)
			.NotEmpty()
			.MaximumLength(3000);
	}
}
