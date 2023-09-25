using System;

namespace Wally.RomMaster.HashService.Domain.Abstractions;

public interface IDateTimeProvider
{
	public DateTime GetDateTime();
}
