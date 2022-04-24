using System;

namespace Wally.RomMaster.BackgroundServices.Abstractions;

public interface IClockService
{
	DateTime StartTimestamp { get; }
}
