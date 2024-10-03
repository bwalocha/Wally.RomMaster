using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class MediaId : GuidId<MediaId>
{
	public MediaId()
	{
	}

	public MediaId(Guid value)
		: base(value)
	{
	}
}
