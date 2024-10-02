using System;

namespace Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

public interface IDateTimeProvider
{
	public DateTimeOffset GetDateTime();
}
