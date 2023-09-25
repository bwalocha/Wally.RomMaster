using System;

using Wally.RomMaster.HashService.Domain.Abstractions;

namespace Wally.RomMaster.HashService.Infrastructure.DI.Microsoft.Providers;

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
