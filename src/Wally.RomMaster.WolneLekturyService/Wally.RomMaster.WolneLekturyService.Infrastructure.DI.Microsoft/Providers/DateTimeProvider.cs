using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft.Providers;

public class DateTimeProvider : IDateTimeProvider
{
	private readonly Func<DateTimeOffset> _function;

	public DateTimeProvider()
	{
		_function = () => DateTimeOffset.UtcNow;
	}

	public DateTimeProvider(Func<DateTimeOffset> function)
	{
		_function = function;
	}

	public DateTimeOffset GetDateTime()
	{
		return _function();
	}
}
