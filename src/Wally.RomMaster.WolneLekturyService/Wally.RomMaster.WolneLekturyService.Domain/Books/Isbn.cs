using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class Isbn : ValueObject<Isbn, string>
{
	public Isbn(string value)
	: base(value)
	{
	}
}
