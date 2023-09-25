using System;

using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Providers;

public class DateTimeProvider : IDateTimeProvider
{
	private readonly Func<DateTime> _function;

	public DateTimeProvider()
	{
		_function = () => DateTime.UtcNow;
	}

	public DateTimeProvider(Func<DateTime> function)
	{
		_function = function;
	}

	public DateTime GetDateTime()
	{
		return _function();
	}
}
