using System;
using Wally.RomMaster.NotificationService.Domain.Abstractions;

namespace Wally.RomMaster.NotificationService.Domain;

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
