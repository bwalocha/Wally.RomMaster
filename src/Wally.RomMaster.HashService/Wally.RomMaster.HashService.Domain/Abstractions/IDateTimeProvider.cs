using System;

namespace Wally.RomMaster.HashService.Domain.Abstractions;

public interface IDateTimeProvider
{
	public DateTimeOffset GetDateTime();
}
