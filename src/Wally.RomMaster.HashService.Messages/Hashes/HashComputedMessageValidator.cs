using FluentValidation;

namespace Wally.RomMaster.HashService.Messages.Hashes;

public class HashComputedMessageValidator : AbstractValidator<HashComputedMessage>
{
	public HashComputedMessageValidator()
	{
		RuleFor(a => a.FileId)
			.NotEmpty();
		RuleFor(a => a.Crc32)
			.NotEmpty()
			.Length(8);
	}
}
