using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class BookId : GuidId<BookId>
{
	public BookId()
	{
	}

	public BookId(Guid value)
		: base(value)
	{
	}
}
