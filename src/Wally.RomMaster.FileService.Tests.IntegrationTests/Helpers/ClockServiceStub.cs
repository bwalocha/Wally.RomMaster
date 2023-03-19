using System;

using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Tests.IntegrationTests.Helpers;

public class ClockServiceStub : IClockService
{
	private readonly Func<DateTime> _clock;

	public ClockServiceStub(Func<DateTime> clock)
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
