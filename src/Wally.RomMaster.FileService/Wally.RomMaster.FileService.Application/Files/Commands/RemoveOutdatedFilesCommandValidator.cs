using System;
using FluentValidation;

namespace Wally.RomMaster.FileService.Application.Files.Commands;

public class RemoveOutdatedFilesCommandValidator : AbstractValidator<RemoveOutdatedFilesCommand>
{
	public RemoveOutdatedFilesCommandValidator()
	{
		RuleFor(a => a.Timestamp)
			.GreaterThan(new DateTime(2000, 1, 1));
	}
}
