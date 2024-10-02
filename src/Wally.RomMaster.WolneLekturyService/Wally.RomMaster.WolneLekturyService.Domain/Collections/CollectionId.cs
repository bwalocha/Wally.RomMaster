using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Collections;

public class CollectionId : GuidId<CollectionId>
{
	public CollectionId()
	{
	}

	public CollectionId(Guid value)
		: base(value)
	{
	}
}
