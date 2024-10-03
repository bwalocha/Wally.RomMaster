using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class Language : ValueObject<Language, string>
{
	public Language(string value)
	: base(value)
	{
	}
}
