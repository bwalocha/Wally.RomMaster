using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Epochs;

public class EpochId : GuidId<EpochId>
{
	public EpochId()
	{
	}

	public EpochId(Guid value)
		: base(value)
	{
	}
}
