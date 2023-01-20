using FluentValidation;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class UpdateHashCommandValidator : AbstractValidator<UpdateHashCommand>
{
	public UpdateHashCommandValidator()
	{
		RuleFor(a => a.FileId)
			.NotEmpty();
		RuleFor(a => a.Crc32)
			.Length(8);
	}
}
