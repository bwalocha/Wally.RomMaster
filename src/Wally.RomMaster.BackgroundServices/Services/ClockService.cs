using System;

using Wally.RomMaster.BackgroundServices.Abstractions;

namespace Wally.RomMaster.BackgroundServices.Services;

public class ClockService : IClockService
{
	public ClockService(DateTime startTimestamp)
	{
		StartTimestamp = startTimestamp;
	}

	public DateTime StartTimestamp { get; }
}
