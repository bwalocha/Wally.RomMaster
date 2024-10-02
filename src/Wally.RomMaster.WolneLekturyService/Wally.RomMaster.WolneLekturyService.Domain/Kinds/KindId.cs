using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Kinds;

public class KindId : GuidId<KindId>
{
	public KindId()
	{
	}

	public KindId(Guid value)
		: base(value)
	{
	}
}
