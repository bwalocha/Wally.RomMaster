using System;

using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.BackgroundServices.Services;

public class ClockService : IClockService
{
	private readonly Func<DateTime> _clock;

	public ClockService(Func<DateTime> clock)
	{
		_clock = clock;
		StartTimestamp = clock();
	}

	public DateTime StartTimestamp { get; }

	public DateTime GetTimestamp()
	{
		return _clock();
	}
}
