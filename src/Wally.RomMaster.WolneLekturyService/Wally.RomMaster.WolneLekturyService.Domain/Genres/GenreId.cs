using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Genres;

public class GenreId : GuidId<GenreId>
{
	public GenreId()
	{
	}

	public GenreId(Guid value)
		: base(value)
	{
	}
}
