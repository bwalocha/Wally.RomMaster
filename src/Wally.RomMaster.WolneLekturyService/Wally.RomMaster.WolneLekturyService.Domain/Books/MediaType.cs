using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class MediaType : ValueObject<MediaType, string>
{
	public MediaType(string value)
	: base(value)
	{
	}
}
