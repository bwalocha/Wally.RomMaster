using System;

namespace Wally.RomMaster.FileService.Domain.Abstractions;

public interface IDateTimeProvider
{
	public DateTimeOffset GetDateTime();
}
