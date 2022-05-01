using System;

namespace Wally.RomMaster.Domain.Abstractions;

public interface IClockService
{
	DateTime StartTimestamp { get; }

	DateTime GetTimestamp();
}
