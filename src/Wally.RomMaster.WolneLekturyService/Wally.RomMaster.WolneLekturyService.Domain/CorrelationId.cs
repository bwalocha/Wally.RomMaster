using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain;

public class CorrelationId : GuidId<CorrelationId>
{
	public CorrelationId()
	{
	}

	public CorrelationId(Guid value)
		: base(value)
	{
	}

	public static explicit operator Guid(CorrelationId id)
	{
		return id.Value;
	}
}
