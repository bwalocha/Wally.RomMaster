using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class BookDetailId : GuidId<BookDetailId>
{
	public BookDetailId()
	{
	}

	public BookDetailId(Guid value)
		: base(value)
	{
	}
}
